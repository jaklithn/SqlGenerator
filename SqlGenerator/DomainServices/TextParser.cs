using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using SqlGenerator.Entities;

namespace SqlGenerator.DomainServices
{
	public static class TextParser
	{
		/// <summary>
		/// Parse values from text file and add to Rows collection.
		/// </summary>
		public static List<FileRow> GetValues(string path, string delimiter, bool skipFirstLine)
		{
			var rowItems = new List<FileRow>();
			if (delimiter.Length != 1)
				throw new Exception(string.Format("Delimiter='{0}' should only be one character", delimiter));

			var lines = File.ReadLines(path, Encoding.Default);

			if (skipFirstLine)
				lines = lines.Skip(1);

			foreach (var line in lines)
			{
				if (!line.Contains(delimiter))
					throw new Exception("Lines don't contain any delimiters of this type");

				var rowItem = new FileRow();
				var values = line.Split(delimiter.ToCharArray(), StringSplitOptions.None);
				rowItem.Values.AddRange(values);
				rowItems.Add(rowItem);
			}
			return rowItems;

		}
	}
}