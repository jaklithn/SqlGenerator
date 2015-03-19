namespace SqlGenerator.Entities
{
	public class SqlColumnValue : SqlColumn
	{
		public string SqlValue { get; private set; }

		public SqlColumnValue(SqlColumn sqlColumn, string sqlValue)
		{
			ColumnName = sqlColumn.ColumnName;
			BaseColumnName = sqlColumn.BaseColumnName;
			BaseTableName = sqlColumn.BaseTableName;
			DataType = sqlColumn.DataType;
			ProviderSpecificDataType = sqlColumn.ProviderSpecificDataType;
			DataTypeName = sqlColumn.DataTypeName;

			ColumnOrdinal = sqlColumn.ColumnOrdinal;
			ColumnSize = sqlColumn.ColumnSize;
			NumericPrecision = sqlColumn.NumericPrecision;
			NumericScale = sqlColumn.NumericScale;

			ProviderType = sqlColumn.ProviderType;
			NonVersionedProviderType = sqlColumn.NonVersionedProviderType;

			IsUnique = sqlColumn.IsUnique;
			IsColumnSet = sqlColumn.IsColumnSet;
			AllowDBNull = sqlColumn.AllowDBNull;
			IsKey = sqlColumn.IsKey;
			IsIdentity = sqlColumn.IsIdentity;
			IsAutoIncrement = sqlColumn.IsAutoIncrement;
			IsRowVersion = sqlColumn.IsRowVersion;
			IsLong = sqlColumn.IsLong;
			IsReadOnly = sqlColumn.IsReadOnly;

			SqlValue = sqlValue;
		}
	}
}