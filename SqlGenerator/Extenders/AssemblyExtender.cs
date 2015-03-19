using System;
using System.Reflection;
using System.Text.RegularExpressions;

namespace SqlGenerator.Extenders
{
	public static class AssemblyExtender
	{
		#region Shortcuts assuming we use the Calling assembly

		/// <summary>
		/// Get display version for calling assembly.
		/// </summary>
		public static string DisplayVersion()
		{
			return Assembly.GetCallingAssembly().DisplayVersion();
		}

		/// <summary>
		/// Get version for calling assembly.
		/// </summary>
		public static Version Version()
		{
			return Assembly.GetCallingAssembly().Version();
		}

		/// <summary>
		/// Get build date for calling assembly.
		/// </summary>
		public static DateTime BuildDate()
		{
			return Assembly.GetCallingAssembly().BuildDate();
		}

		/// <summary>
		/// Get name of calling assembly.
		/// </summary>
		public static string AssemblyName()
		{
			return Assembly.GetCallingAssembly().GetName().Name;
		}

		/// <summary>
		/// Get company of calling assembly.
		/// </summary>
		public static string AssemblyCompany()
		{
			return Assembly.GetCallingAssembly().AssemblyCompany();
		}

		/// <summary>
		/// Get copyright of calling assembly.
		/// </summary>
		public static string AssemblyCopyright()
		{
			return Assembly.GetCallingAssembly().AssemblyCopyright();
		}

		#endregion



		#region Extension methods

		public static Version Version(this Assembly assembly)
		{
			return assembly.GetName().Version;
		}

		public static string DisplayVersion(this Assembly assembly)
		{
			var version = assembly.Version();
			return string.Format("{0}.{1}", version.Major, version.Minor);
		}

		/// <summary>
		/// Retrieve the assembly build date.
		/// Note: This will only work for assemblies where the Build and Revision of the Version is auto generated!
		/// Example: [assembly: AssemblyVersion("4.2.*")]
		/// </summary>
		public static DateTime BuildDate(this Assembly assembly)
		{
			var version = assembly.Version();
			return new DateTime(2000, 1, 1).AddDays(version.Build).AddSeconds(version.Revision * 2);
		}

		public static string AssemblyTitle(this Assembly assembly)
		{
			return assembly.GetAssemblyProperty<AssemblyTitleAttribute>().ToString();
		}

		public static string AssemblyProduct(this Assembly assembly)
		{
			return assembly.GetAssemblyProperty<AssemblyProductAttribute>().ToString();
		}

		public static string AssemblyCopyright(this Assembly assembly)
		{
			return assembly.GetAssemblyProperty<AssemblyCopyrightAttribute>().ToString();
		}

		public static string AssemblyCompany(this Assembly assembly)
		{
			return assembly.GetAssemblyProperty<AssemblyCompanyAttribute>().ToString();
		}

		#endregion




		#region Private Methods

		private static object GetAssemblyProperty<T>(this Assembly assembly) where T : class
		{
			var attributeName = typeof(T).Name;
			var match = Regex.Match(attributeName, "(Assembly)([A-Za-z]*)(Attribute)");
			var propertyName = match.Groups[2].ToString();
			var attributes = assembly.GetCustomAttributes(typeof(T), false);

			// If there is at least one Name attribute
			if(attributes.Length > 0)
			{
				// Select the first one
				var attribute = (T)attributes[0];
				return attribute.GetPropertyValue(propertyName);
			}

			// Fallback 
			return string.Empty;
		}

		#endregion

	}
}