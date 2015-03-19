using System.Linq;

namespace SqlGenerator.Extenders
{
	public static class ObjectExtender
	{
		public static object GetPropertyValue(this object obj, string propertyName)
		{
			return obj.GetType().GetProperties().Single(pi => pi.Name == propertyName).GetValue(obj, null);
		}
	}
}