using System.Collections.Generic;
using System.Reflection;
using System.Xml;
using ICSharpCode.TextEditor.Document;


namespace SqlGenerator.DomainServices
{
	public class AppSyntaxModeProvider : ISyntaxModeFileProvider
	{
		public ICollection<SyntaxMode> SyntaxModes { get; private set; }

		public AppSyntaxModeProvider()
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourcePath = string.Format("{0}.Resources.SyntaxModes.xml", assembly.GetName().Name);
			var syntaxModeStream = assembly.GetManifestResourceStream(resourcePath);
			SyntaxModes = syntaxModeStream == null ? new List<SyntaxMode>() : SyntaxMode.GetSyntaxModes(syntaxModeStream);
		}

		public XmlTextReader GetSyntaxModeFile(SyntaxMode syntaxMode)
		{
			var assembly = Assembly.GetExecutingAssembly();
			var resourcePath = string.Format("{0}.Resources.{1}", assembly.GetName().Name, syntaxMode.FileName);
			using (var stream = assembly.GetManifestResourceStream(resourcePath))
			{
				return stream == null ? null : new XmlTextReader(stream);
			}
		}

		public void UpdateSyntaxModeList()
		{
			// Resources don't change during runtime
		}
	}
}