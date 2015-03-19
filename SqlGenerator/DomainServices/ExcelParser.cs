using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.IO;
using Excel;
using SqlGenerator.Entities;


namespace SqlGenerator.DomainServices
{
	/// <summary>
	/// Excel parsing in this class is performed by using a common shareware Lib.
	/// It is MUCH faster than using Microsoft Excel automation!!
	/// http://exceldatareader.codeplex.com/
	/// Excel.dll, Excel.pdb, ICSharpCode.SharpZipLib.dll
	/// Current version is taken from downloaded source 2013-01-29
	/// </summary>
	public static class ExcelParser
	{
		/// <summary>
		/// Parse sheet names from given Excel file.
		/// </summary>
		public static List<string> GetSheetNames(string path)
		{
			using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				using (var excelReader = GetExcelDataReader(path, stream))
				{
					excelReader.IsFirstRowAsColumnNames = true;
					var dataset = excelReader.AsDataSet();
					var names = from DataTable table in dataset.Tables
								select table.TableName;
					return names.ToList();
				}
			}
		}

		/// <summary>
		/// Parse values from Excel sheet and add to Rows collection.
		/// </summary>
		public static List<FileRow> GetValues(string path, string sheetName, bool skipFirstLine)
		{
			var rowItems = new List<FileRow>();
			using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.Read))
			{
				using (var excelReader = GetExcelDataReader(path, stream))
				{
					excelReader.IsFirstRowAsColumnNames = skipFirstLine;
					var dataset = excelReader.AsDataSet();
					foreach (DataRow row in dataset.Tables[sheetName].Rows)
					{
						var rowItem = new FileRow();
						foreach (var value in row.ItemArray)
							rowItem.Values.Add(value);
						rowItems.Add(rowItem);
					}
				}
			}
			return rowItems;
		}

		private static IExcelDataReader GetExcelDataReader(string path, Stream stream)
		{
			var extension = GetExtension(path);
			switch (extension)
			{
				case "xls":
					return ExcelReaderFactory.CreateBinaryReader(stream);
				case "xlsx":
					return ExcelReaderFactory.CreateOpenXmlReader(stream);
				default:
					throw new Exception(string.Format("'{0}' is not a valid Excel extension", extension));
			}
		}

		private static string GetExtension(string path)
		{
			var extension = Path.GetExtension(path);
			return extension == null ? null : extension.ToLower().Substring(1);
		}
	}
}