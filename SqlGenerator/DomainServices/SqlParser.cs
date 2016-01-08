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
	public class SqlParser
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
                    Debug.WriteLine("{0}, {1}, {2}, {3}", database, schema, name, type);

					if (type == "BASE TABLE" && name != "sysdiagrams")
					{
						var tableSpecifier = schema == "dbo" ? name : string.Format("{0}.{1}", schema, name);
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
                    cmd.CommandText = string.Format("SELECT * FROM {0}", tableName);
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
                    var whereCondition = filterCondition.IsSpecified() ? " WHERE " + filterCondition.Trim(): string.Empty;
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = string.Format("SELECT * FROM {0}{1}", tableName, whereCondition);
                    cmd.CommandTimeout = 60;

                    var fileRows= new List<FileRow>();
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
				Debug.WriteLine(string.Format("MissingProperties: {0}", string.Join(", ", missingProperties)));
			}

			return sqlColumns;
		}
	}
}