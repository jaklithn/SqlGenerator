using System.Reflection;
using System.Runtime.InteropServices;

[assembly: AssemblyTitle("SqlGenerator")]
[assembly: AssemblyProduct("SqlGenerator")]
[assembly: AssemblyDescription("Generate SQL statements from common input sources")]
[assembly: AssemblyCompany("Jakob Lithner")]
[assembly: AssemblyCopyright("Created by Jakob Lithner 2013-2020, Open Source")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]
[assembly: ComVisible(false)]


// Version: Major.Minor.Build.Revision
// Note:    Build and Revision must be automatically generated to make the BuildDate property work!
[assembly: AssemblyVersion("1.8.*")]
[assembly: AssemblyFileVersion("1.8.0.0")]


// File versions
// ========================================================================================================
// 1.8		Ensure valid table order for inserted statements
// 1.7		Update nuget component ExcelDataReader (fix v2.x to 3.6.0) and add ExcelDataReader.Dataset. Update to .NET 4.8
// 1.6		Allow connections to be edited in application.
// 1.5		Parse table schema from SQL Server and create a mapping scenario.
// 1.4		Remove settings file.
// 1.3		Correct parsing of decimal values.
// 1.2		Corrected parsing of rows with empty values. Allowed types to be surrounded by brackets. Allowed NULL values to be stated explicitly. Added validation of expected delimiter.
// 1.1		Added version checker, clear button, splitter and DelimiterType.
// 1.0		First distributed version.
