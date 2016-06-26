using System.Collections.Generic;


namespace SqlGenerator.Entities
{
	public class FileRow
	{
		public List<object> Values { get; set; }

		public FileRow()
		{
			Values = new List<object>();
		}
	}
}