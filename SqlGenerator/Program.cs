using System;
using System.Windows.Forms;
using SqlGenerator.Forms;


namespace SqlGenerator
{
	internal static class Program
	{
		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(false);
			Application.Run(new Start());
		}
	}
}