//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.IO;
//using SqlGenerator.Entities;
//using Net.SourceForge.Koogra;


//namespace SqlGenerator.DomainServices
//{
//	/// <summary>
//	/// Excel parsing in this class is performed by using a common shareware Lib found here: http://koogra.sourceforge.net/
//	/// The files used are: Net.SourceForge.Koogra.dll, Net.SourceForge.Koogra.xml, Ionic.Utils.Zip.dll
//	/// </summary>
//	public static class ExcelParser2
//	{
//		/// <summary>
//		/// Parse sheet names from given Excel file.
//		/// </summary>
//		public static List<string> GetSheetNames(string path)
//		{
//			var wb = GetWorkbook(path);
//			return wb.Worksheets.EnumerateWorksheetNames().ToList();
//		}

//		/// <summary>
//		/// Parse values from Excel sheet and add to FileContent Rows collection.
//		/// </summary>
//		public static void GetValues(FileContent table, string path, string sheetName, bool firstRowHoldsColumnNames = true)
//		{
//			var wb = GetWorkbook(path);
//			var ws = wb.Worksheets.GetWorksheetByName(sheetName);
//			var firstRow = firstRowHoldsColumnNames ? ws.FirstRow + 1 : ws.FirstRow;

//			for (var rowId = firstRow; rowId <= ws.LastRow; ++rowId)
//			{
//				var rowItem = new RowItem();
//				var row = ws.Rows.GetRow(rowId);
//				for (var colId = ws.FirstCol; colId <= ws.LastCol; ++colId)
//				{
//					var value = row.GetCell(colId).Value;
//					rowItem.Values.Add(value);
//					//if (!row.GetCell(colId).Value.Equals(row.GetCell(colId).GetFormattedValue()))
//					//	Debug.WriteLine("{0} != {1}", row.GetCell(colId).Value, row.GetCell(colId).GetFormattedValue());
//				}
//				table.Rows.Add(rowItem);
//			}
//		}

//		private static IWorkbook GetWorkbook(string path)
//		{
//			var extension = GetExtension(path);
//			switch (extension)
//			{
//				case "xls":
//					return WorkbookFactory.GetExcelBIFFReader(path);
//				case "xlsx":
//					return WorkbookFactory.GetExcel2007Reader(path);
//				default:
//					throw new Exception(string.Format("'{0}' is not a valid Excel extension", extension));
//			}
//		}

//		private static string GetExtension(string path)
//		{
//			var extension = Path.GetExtension(path);
//			return extension == null ? null : extension.ToLower().Substring(1);
//		}
//	}
//}