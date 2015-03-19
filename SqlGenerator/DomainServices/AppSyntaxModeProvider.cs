using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using ICSharpCode.TextEditor.Document;

namespace SqlGenerator.DomainServices
{
	public class AppSyntaxModeProvider : ISyntaxModeFileProvider
	{
		private readonly List<SyntaxMode> _syntaxModes;

		public ICollection<SyntaxMode> SyntaxModes
		{
			get
			{
				return _syntaxModes;
			}
		}

		public AppSyntaxModeProvider()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourcePath = string.Format("{0}.Resources.SyntaxModes.xml", assembly.GetName().Name);
			var syntaxModeStream = assembly.GetManifestResourceStream(resourcePath);
			_syntaxModes = syntaxModeStream != null ? SyntaxMode.GetSyntaxModes(syntaxModeStream) : new List<SyntaxMode>();
		}

		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourcePath = string.Format("{0}.Resources.{1}", assembly.GetName().Name, syntaxMode.FileName);
			var stream = assembly.GetManifestResourceStream(resourcePath);
			return new XmlTextReader(stream);
		}

		public void UpdateSyntaxModeList()
		{
			// resources don't change during runtime
		}
	}
}