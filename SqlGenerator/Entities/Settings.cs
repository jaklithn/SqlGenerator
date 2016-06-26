using System.Collections.Generic;
using System.Linq;


namespace SqlGenerator.Entities
{
	public class Settings
	{
		public List<string> ConnectionStrings { get; set; }
		public string TableName { get; set; }
		public string FilePath { get; set; }
		public string SheetName { get; set; }
		public string ConnectionKey { get; set; }
		public string FileType { get; set; }
		public string Delimiter { get; set; }
		public string SelectedConnectionString { get; set; }

		public Settings()
		{
			ConnectionStrings = new List<string>();
			TableName = TableName;
			FilePath = @"C:\Temp\MyExcelFile.xlsx";
			SheetName = "Sheet1";
			ConnectionKey = "Northwind";
			FileType = "Excel";
			Delimiter = "Semicolon";
			SelectedConnectionString = "Data Source=localhost;Initial Catalog=Northwind;Trusted_Connection=True;";
		}

		public List<ConnectionItem> GetConnectionItems()
		{
			return ConnectionStrings.Select(x => new ConnectionItem(x)).OrderBy(c => c.DisplayName).ToList();
		}
	}
}