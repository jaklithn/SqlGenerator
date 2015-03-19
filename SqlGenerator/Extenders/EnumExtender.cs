using System;
using System.Collections.Generic;
using System.Linq;

namespace SqlGenerator.Extenders
{
	public static class EnumExtender
	{
		/// <summary>
		/// Convert provided enum type to list of values.
		/// This is convenient when you need to iterate enum values.
		/// </summary>
		public static List<T> ToList<T>()
		{
			if (!typeof(T).IsEnum)
				throw new ArgumentException();
			return Enum.GetValues(typeof(T)).Cast<T>().ToList();
		}
	}
}