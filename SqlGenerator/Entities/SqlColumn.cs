using System;
using System.Text.RegularExpressions;
using System.Threading;


namespace SqlGenerator.Entities
{
    public class SqlColumn
    {
        public string ColumnName { get; set; }
        public string BaseColumnName { get; set; }
        public string BaseTableName { get; set; }
        public string DataType { get; set; }
        public string ProviderSpecificDataType { get; set; }
        public string DataTypeName { get; set; }

        public int ColumnOrdinal { get; set; }
        public int ColumnSize { get; set; }
        public int NumericPrecision { get; set; }
        public int NumericScale { get; set; }

        public int ProviderType { get; set; }
        public int NonVersionedProviderType { get; set; }

        public bool IsUnique { get; set; }
        public bool IsColumnSet { get; set; }
        // ReSharper disable once InconsistentNaming
        public bool AllowDBNull { get; set; }
        public bool IsKey { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsAutoIncrement { get; set; }
        public bool IsRowVersion { get; set; }
        public bool IsLong { get; set; }
        public bool IsReadOnly { get; set; }


        public string DisplayDataType
        {
            get
            {
                switch (DataTypeName.ToLower())
                {
                    case "varchar":
                    case "char":
                    case "text":
                    case "nvarchar":
                    case "nchar":
                    case "ntext":
                        return $"{DataTypeName}({ColumnSize})";

                    case "real":
                    case "float":
                    case "numeric":
                    case "decimal":
                        return $"{DataTypeName}[{NumericPrecision},{NumericScale}]";

                    default:
                        return DataTypeName;
                }
            }
        }

        public string CreateValueString(object value)
        {
            if (value == DBNull.Value || value.ToString().Trim().ToUpper() == "NULL")
                return "NULL";

            switch (DataTypeName.ToLower())
            {
                case "uniqueidentifier":
                case "varchar":
                case "char":
                case "text":
                    return $"'{value.ToString().Replace("'", "''")}'";

                case "nvarchar":
                case "nchar":
                case "ntext":
                    return $"N'{value.ToString().Replace("'", "''")}'";

                case "money":
                case "smallmoney":
                    return value.ToString().Replace(',', '.');

                case "float":
                case "real":
                    var strValueF = FormatDecimal(value);
                    return $"CAST({strValueF} AS Float({NumericPrecision})";

                case "numeric":
                case "decimal":
                    var strValueD = FormatDecimal(value);
                    return $"CAST({strValueD} AS Decimal({NumericPrecision}, {NumericScale}))";

                case "bit":
                    var boolValue = Convert.ToBoolean(value);
                    return boolValue ? "1" : "0";

                case "datetime":
                    DateTime datValue;
                    if (!DateTime.TryParse(value.ToString(), out datValue))
                    {
                        // Excel can internally store DateTime as double
                        var decValue = GetDouble(value);
                        datValue = DateTime.FromOADate(decValue);
                    }
                    return $"'{datValue.ToString("yyyy-MM-dd HH:mm:ss")}'";

                default:
                    return value.ToString();
            }
        }

        /// <summary>
        /// Ensure dynamic number format from Excel is transformed into correct format.
        /// Very complicated way ...
        /// </summary>
        private string FormatDecimal(object value)
        {
            var decValue = GetDouble(value);
            var format = string.Format("{{0:F{0}}}", NumericScale);
            var strValue = string.Format(format, decValue);
            strValue = strValue.Replace(',', '.');

            return strValue;
        }

        private static double GetDouble(object value)
        {
            var strValue = value.ToString();
            var culture = Thread.CurrentThread.CurrentCulture;

            const string pattern = "[.,]";
            var replacement = culture.NumberFormat.NumberDecimalSeparator;

            var regex = new Regex(pattern);
            strValue = regex.Replace(strValue, replacement);

            try
            {
                var decValue = double.Parse(strValue, culture.NumberFormat);
                return decValue;
            }
            catch (Exception)
            {
                throw new ArgumentException($"Failed to parse the following value: {strValue}");
            }
        }
    }
}