using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using SqlGenerator.Entities;
using SqlGenerator.Extenders;


namespace SqlGenerator.DomainServices
{
    public static class SqlParser
    {
        public static List<string> GetTables(string connectionString)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                var tables = new List<string>();
                var dt = con.GetSchema("Tables");
                foreach (DataRow row in dt.Rows)
                {
                    var database = row[0].ToString();
                    var schema = row[1].ToString();
                    var name = row[2].ToString();
                    var type = row[3].ToString();
                    Debug.WriteLine($"{database}, {schema}, {name}, {type}");

                    if (type == "BASE TABLE" && name != "sysdiagrams")
                    {
                        var tableSpecifier = schema == "dbo" ? name : $"{schema}.{name}";
                        tables.Add(tableSpecifier);
                    }
                }
                return tables.OrderBy(t => t).ToList();
            }
        }

        public static List<SqlColumn> GetColumns(string connectionString, string tableName)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM {tableName}";
                    cmd.CommandTimeout = 60;

                    using (var dr = cmd.ExecuteReader(CommandBehavior.KeyInfo))
                    {
                        var schemaTable = dr.GetSchemaTable();
                        return ParseColumns(schemaTable);
                    }
                }
            }
        }

        public static List<FileRow> GetValues(string connectionString, string tableName, string filterCondition)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand())
                {
                    var whereCondition = filterCondition.IsSpecified() ? " WHERE " + filterCondition.Trim() : string.Empty;
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = $"SELECT * FROM {tableName}{whereCondition}";
                    cmd.CommandTimeout = 60;

                    var fileRows = new List<FileRow>();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            var rowItem = new FileRow();
                            for (var i = 0; i < dr.FieldCount; i++)
                            {
                                rowItem.Values.Add(dr.GetValue(i));
                            }
                            fileRows.Add(rowItem);
                        }
                    }
                    return fileRows;
                }
            }
        }

        private static List<SqlColumn> ParseColumns(DataTable schemaTable)
        {
            var sqlColumns = new List<SqlColumn>();
            var typeProperties = new[] { "DataType", "ProviderSpecificDataType" };
            var decimalTypes = new[] { "real", "float", "decimal", "money", "smallmoney" };
            var missingProperties = new HashSet<string>();


            foreach (DataRow column in schemaTable.Rows)
            {
                var sqlColumn = new SqlColumn();

                foreach (DataColumn property in schemaTable.Columns)
                {
                    var propertyName = property.ColumnName;
                    var value = column[propertyName];

                    // Manual adjustment to skip namespace on type properties
                    if (typeProperties.Contains(propertyName))
                    {
                        value = ((Type)value).FullName;
                    }

                    var propertyInfo = typeof(SqlColumn).GetProperty(propertyName);
                    if (!Convert.IsDBNull(value) && propertyInfo != null)
                    {
                        propertyInfo.SetValue(sqlColumn, value, null);
                    }
                    else
                    {
                        missingProperties.Add(propertyName);
                    }
                }

                // Manual adjustment to make scale/precision more understandable for decimal data types
                if (decimalTypes.Contains(sqlColumn.DataTypeName))
                {
                    sqlColumn.NumericScale = 0;
                    sqlColumn.NumericPrecision = 0;
                }
                sqlColumns.Add(sqlColumn);
            }

            if (missingProperties.Count > 0)
            {
                Debug.WriteLine($"MissingProperties: {string.Join(", ", missingProperties)}");
            }

            return sqlColumns;
        }

        public static List<string> GetTablesOrderedForInsert(string connectionString)
        {
            var allTables = GetTables(connectionString);
            var tableDependencies = GetTableDependencies(connectionString);

            var resultingTables = new List<string>();

            do
            {
                var foreignTables = tableDependencies.Select(x => x.Foreign).ToList();
                var relatedTables = tableDependencies.SelectMany(x => x.Related).Distinct().ToList();
                var independantForeigntables = foreignTables.Except(relatedTables).ToList();

                var hasCircularDependencies = relatedTables.Any() && !independantForeigntables.Any();
                if (hasCircularDependencies)
                {
                    var suggestedTopParent = tableDependencies.OrderByDescending(x => x.Related.Count).Take(1).Single().Foreign;
                    independantForeigntables.Add(suggestedTopParent);
                }

                resultingTables.AddRange(independantForeigntables.OrderBy(x => x));
                tableDependencies.RemoveAll(x => independantForeigntables.Contains(x.Foreign));
            } while (tableDependencies.Any());

            var stillMissingTables = allTables.Except(resultingTables).ToList();
            resultingTables.AddRange(stillMissingTables);

            return resultingTables;
        }


        private static List<TableDependency> GetTableDependencies(string connectionString)
        {
            using (var con = new SqlConnection(connectionString))
            {
                con.Open();
                using (var cmd = new SqlCommand())
                {
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT ObjectSchema.name AS [Schema], Object.name AS Name, ForeignSchema.name AS ForeignSchema, ForeignObject.name AS ForeignName " +
                                      "FROM sys.objects AS ForeignObject INNER JOIN " +
                                      "sys.foreign_key_columns ON ForeignObject.object_id = sys.foreign_key_columns.referenced_object_id " +
                                      "INNER JOIN sys.objects AS Object ON sys.foreign_key_columns.parent_object_id = Object.object_id " +
                                      "INNER JOIN sys.schemas AS ObjectSchema ON Object.schema_id = ObjectSchema.schema_id " +
                                      "INNER JOIN sys.schemas AS ForeignSchema ON ForeignObject.schema_id = ForeignSchema.schema_id";

                    var tableDependencies = new List<TableDependency>();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {

                            var schema = dr.GetString(0);
                            var name = dr.GetString(1);
                            var foreignSchema = dr.GetString(2);
                            var foreignName = dr.GetString(3);

                            // Add schema if needed
                            var specifier = schema == "dbo" ? name : $"{schema}.{name}";
                            var foreignSpecifier = foreignSchema == "dbo" ? foreignName : $"{foreignSchema}.{foreignName}";

                            var foreignRecord = tableDependencies.SingleOrDefault(x => x.Foreign == foreignSpecifier);
                            if (foreignRecord == null)
                            {
                                foreignRecord = new TableDependency { Foreign = foreignSpecifier };
                                tableDependencies.Add(foreignRecord);
                            }
                            foreignRecord.Related.Add(specifier);
                        }
                    }
                    return tableDependencies;
                }
            }
        }

    }
}