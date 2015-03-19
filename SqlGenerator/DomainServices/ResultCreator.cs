using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlGenerator.Controls;
using SqlGenerator.Entities;
using SqlGenerator.Entities.Enums;


namespace SqlGenerator.DomainServices
{
	public static class ResultCreator
	{
		public static string GetResult(CommandType commandType, List<FileRow> fileRows, List<ColumnMap> columnMaps, string tableName)
		{
			var sb = new StringBuilder();

			foreach (var fileRow in fileRows)
			{
				if (IsEmpty(fileRow))
				{
					continue;
				}

				var format = GetFormat(commandType, tableName, columnMaps);
				var adjustedValues = AdjustedValues(columnMaps, fileRow.Values);
				var line = string.Format(format, adjustedValues.ToArray());

				sb.AppendLine(line);
			}

			return sb.ToString().Trim();
		}


		private static string GetFormat(CommandType commandType, string tableName, List<ColumnMap> columnMaps)
		{
			switch (commandType)
			{
				case CommandType.Insert:
					var columnNames = columnMaps.Select(c => string.Format("[{0}]", c.SqlColumn.ColumnName)).ToList();
					var valueMarkers = columnMaps.Select(c => c.ValueMarker).ToList();
					return string.Format("INSERT INTO {0} ({1}) VALUES ({2})", GetTable(tableName), string.Join(", ", columnNames), string.Join(", ", valueMarkers));

				case CommandType.Update:
					var valuePairs = GetPairs(columnMaps.Where(c => !c.SqlColumn.IsKey));
					var keyPairs = GetPairs(columnMaps.Where(c => c.SqlColumn.IsKey));
					return string.Format("UPDATE {0} SET {1} WHERE {2}", GetTable(tableName), string.Join(", ", valuePairs), string.Join(" AND ", keyPairs));

				case CommandType.Delete:
					var deleteKeyPairs = GetPairs(columnMaps.Where(c => c.SqlColumn.IsKey));
					return string.Format("DELETE FROM {0} WHERE {1}", GetTable(tableName), string.Join(" AND ", deleteKeyPairs));
			}
			return string.Empty;
		}

		private static string GetTable(string tableName)
		{
			var sections = tableName.Split(".[]".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
			return string.Join(".", sections.Select(s => string.Format("[{0}]", s)));
		}

		private static List<object> AdjustedValues(List<ColumnMap> columnMaps, IReadOnlyList<object> values)
		{
			try
			{
				var adjustedValues = new List<object>();
				for (var i = 0; i < values.Count; i++)
				{
					var value = values[i];
					var columnMap = columnMaps.SingleOrDefault(c => c.FileColumnIndex == i);
					var adjustedValue = columnMap == null ? value.ToString() : columnMap.SqlColumn.CreateValueString(value);
					adjustedValues.Add(adjustedValue);
				}
				return adjustedValues;
			}
			catch (InvalidOperationException ex)
			{
				if (ex.Message == "Sequence contains more than one matching element")
					throw new InvalidOperationException("You have mapped the same input column to more than one table column");
				throw;
			}
		}

		private static IEnumerable<string> GetPairs(IEnumerable<ColumnMap> columnMaps)
		{
			return columnMaps.Select(columnMap => string.Format("[{0}]={1}", columnMap.SqlColumn.ColumnName, columnMap.ValueMarker)).ToList();
		}

		private static bool IsEmpty(FileRow fileRow)
		{
			return fileRow.Values.All(t => t == DBNull.Value || t.ToString() == string.Empty);
		}
	}
}