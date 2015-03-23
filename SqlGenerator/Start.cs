using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using SqlGenerator.Controls;
using SqlGenerator.DomainServices;
using SqlGenerator.Entities;
using SqlGenerator.Entities.Enums;
using SqlGenerator.Extenders;
using ICSharpCode.TextEditor.Document;
using SqlGenerator.Properties;


namespace SqlGenerator
{
	public partial class Start : Form
	{
		private bool _isLoading;


		#region Properties

		private FileType FileType
		{
			get
			{
				if (optExcel.Checked)
					return FileType.Excel;
				if (optText.Checked)
					return FileType.Text;
				throw new Exception("Unknown FileType");
			}
			set
			{
				switch (value)
				{
					case FileType.Excel:
						optExcel.Checked = true;
						break;
					case FileType.Text:
						optText.Checked = true;
						break;
					default:
						throw new Exception(string.Format("FileType={0} is not handled", value));
				}
			}
		}

		private DelimiterType Delimiter
		{
			get
			{
				return cboDelimiter.Text.ToEnum<DelimiterType>();
			}
			set
			{
				try
				{
					cboDelimiter.Text = value.ToString();
					if (cboDelimiter.Text.IsNullOrEmpty())
						cboDelimiter.Text = DelimiterType.Semicolon.ToString();
				}
				catch (Exception)
				{
					cboDelimiter.Text = DelimiterType.Semicolon.ToString();
				}
			}
		}

		private List<ColumnMap> MappedColumns
		{
			get
			{
				var mappedColumns = flowColumns.Controls.Cast<ColumnMap>().Where(c => c.IsMapped).ToList();
				return mappedColumns;
			}
		}

		#endregion



		#region Methods

		private void Initialize()
		{
			LoadConnections();
			GetSettings();
			LoadSheetNames();
			SetShowState();

			// Add SQL syntax for text controls
			HighlightingManager.Manager.AddSyntaxModeFileProvider(new AppSyntaxModeProvider());
			txtResult.SetHighlighting("SQL");

			Application.DoEvents();
			btnSearchFile.Focus();
		}

		private void PickFile()
		{
			try
			{
				string title;
				string defaultExt;
				string filter;
				switch (FileType)
				{
					case FileType.Excel:
						title = "Select the Excel file holding values for the SQL generation";
						defaultExt = "xlsx";
						filter = "Excel Files|*.xlsx;*.xls|All Files (*.*)|*.*";
						break;
					case FileType.Text:
						title = "Select the text file holding values for the SQL generation";
						defaultExt = "txt";
						filter = "Text Files|*.txt;*.csv;*.tab|All Files (*.*)|*.*";
						break;
					default:
						throw new Exception(string.Format("FileType={0} is not handled", FileType));
				}

				var dlg = new OpenFileDialog
				{
					Title = title,
					DefaultExt = defaultExt,
					Filter = filter,
					CheckFileExists = true
				};

				if (File.Exists(txtFilePath.Text))
				{
					dlg.FileName = Path.GetFileName(txtFilePath.Text);
					var dir = Path.GetDirectoryName(txtFilePath.Text);
					if (dir != null && Directory.Exists(dir))
						dlg.InitialDirectory = dir;
				}

				if (dlg.ShowDialog() == DialogResult.OK)
				{
					txtFilePath.Text = dlg.FileName;
					if (FileType == FileType.Text && dlg.FileName != null)
					{
						var extension = Path.GetExtension(dlg.FileName).ToLower();
						switch (extension)
						{
							case ".csv":
							case ".txt":
								Delimiter = DelimiterType.Semicolon;
								break;
							case ".tab":
								Delimiter = DelimiterType.Tab;
								break;
						}
					}
					if (FileType == FileType.Excel)
					{
						LoadSheetNames();
					}
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Pick File", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void LoadSheetNames()
		{
			try
			{
				if (FileType == FileType.Excel && File.Exists(txtFilePath.Text) && !_isLoading)
				{
					var sheetNames = ExcelParser.GetSheetNames(txtFilePath.Text);
					cboSheetName.DataSource = sheetNames;
					cboSheetName.SelectedIndex = sheetNames.Count > 0 ? 0 : -1;
				}
				else
				{
					cboSheetName.DataSource = null;
					cboSheetName.SelectedIndex = -1;
				}
			}
			catch (IOException ex)
			{
				if (ex.Message.Contains("The process cannot access the file"))
					if (
						MessageBox.Show("The specified Excel file is open.\r\nPlease close the file before you retry parsing sheet names!\r\n(Cancel will close the application)",
							"Sheet names can not be parsed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry)
						LoadSheetNames();
					else
						Application.Exit();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Generate", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private void ParseSqlColumns()
		{
			try
			{
				flowColumns.Controls.Clear();
				var fileColumns = ParseInputColumns();
				var sqlColumns = SqlParser.GetColumns(cboConnection.SelectedValue.ToString(), cboTableName.Text);
				foreach (var sqlColumn in sqlColumns)
				{
					var columnMap = new ColumnMap(sqlColumn, fileColumns);
					columnMap.IsModified += columnMap_IsModified;
					flowColumns.Controls.Add(columnMap);
				}
				SetShowState();
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "ParseSqlColumns", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
		}

		private List<FileColumn> ParseInputColumns()
		{
			var rowItems = ParseFileContent(false);
			var inputColumns = rowItems[0].Values.Select((value, index) => new FileColumn { Index = index, Name = value.ToString() }).ToList();
			return inputColumns;
		}

		private void Generate(CommandType commandType)
		{
			try
			{
				if (File.Exists(txtFilePath.Text))
				{
					Cursor = Cursors.WaitCursor;
					txtResult.Text = string.Empty;
					SaveSettings();

					var fileContent = ParseFileContent(chkFirstLineHoldsColumnNames.Checked);

					var result = ResultCreator.GetResult(commandType, fileContent, MappedColumns, cboTableName.Text);
					txtResult.Text = result;
					lblResultCount.Text = string.Format("{0} records generated at {1}", txtResult.ActiveTextAreaControl.TextArea.Document.TotalNumberOfLines, DateTime.Now.ToString("HH:mm:ss"));
					SetShowState();
				}
				else
				{
					throw new Exception("Specified file does not exist");
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show(ex.Message, "Generate", MessageBoxButtons.OK, MessageBoxIcon.Error);
			}
			finally
			{
				Cursor = Cursors.Default;
				txtResult.Refresh();
			}
		}

		private List<FileRow> ParseFileContent(bool skipFirstLine)
		{
			switch (FileType)
			{
				case FileType.Excel:
					return ExcelParser.GetValues(txtFilePath.Text, cboSheetName.Text, skipFirstLine);
				case FileType.Text:
					return TextParser.GetValues(txtFilePath.Text, GetDelimiter(), skipFirstLine);
				default:
					throw new Exception(string.Format("FileType={0} is not handled", FileType));
			}
		}

		private void SetShowState()
		{
			lblDelimiter.Visible = false;
			cboDelimiter.Visible = false;
			lblSheetName.Visible = false;
			cboSheetName.Visible = false;
			switch (FileType)
			{
				case FileType.Excel:
					lblSheetName.Visible = true;
					cboSheetName.Visible = true;
					btnGenerateInsert.Enabled = txtFilePath.Text.IsSpecified() && cboSheetName.Text.IsSpecified();
					break;
				case FileType.Text:
					lblDelimiter.Visible = true;
					cboDelimiter.Visible = true;
					btnGenerateInsert.Enabled = txtFilePath.Text.IsSpecified();
					break;
				default:
					throw new Exception(string.Format("FileType={0} is not handled", FileType));
			}
			myToolTip.SetToolTip(btnSearchFile, string.Format("Select {0} file", FileType));

			grpColumnMap.Text = string.Format("Map incoming {0} to table columns by making appropriate choices", chkFirstLineHoldsColumnNames.Checked ? "columns" : "demo data");

			var hasValidColumns = MappedColumns.Any() && MappedColumns.All(c => c.IsValid);
			btnGenerateInsert.Enabled = hasValidColumns;
			btnGenerateUpdate.Enabled = hasValidColumns;
			btnGenerateDelete.Enabled = hasValidColumns;

			btnCopy.Enabled = txtResult.Text.IsSpecified();
		}

		private void LoadConnections()
		{
			var connections = new List<ConnectionItem>();
			foreach (ConnectionStringSettings c in ConfigurationManager.ConnectionStrings)
			{
				if (c.Name == "LocalSqlServer")
					continue;
				var builder = new SqlConnectionStringBuilder(c.ConnectionString);
				connections.Add(new ConnectionItem
				{
					Name = string.Format("{0}   (Server={1}; Database={2})", c.Name, builder.DataSource, builder.InitialCatalog),
					ConnectionString = c.ConnectionString
				});
			}

			cboConnection.DisplayMember = "Name";
			cboConnection.ValueMember = "ConnectionString";
			cboConnection.DataSource = connections;
		}

		private void LoadTables()
		{
			cboTableName.DataSource = SqlParser.GetTables(cboConnection.SelectedValue.ToString());
		}

		private void GetSettings()
		{
			_isLoading = true;


			FileType = Settings.Default.FileType.ToEnum<FileType>();
			txtFilePath.Text = Settings.Default.FilePath;
			LoadSheetNames();
			Delimiter = Settings.Default.Delimiter.ToEnum<DelimiterType>();
			cboSheetName.Text = Settings.Default.SheetName;
			LoadConnections();
			cboConnection.SelectedValue = Settings.Default.ConnectionString;
			cboTableName.Text = Settings.Default.TableName;


			if (Environment.UserName == "jaklithn")
			{
				// Test data during development
				FileType = FileType.Excel;
				txtFilePath.Text = @"C:\Temp\Customers.xlsx";
				Delimiter = DelimiterType.Semicolon;
				cboSheetName.Text = "Customers";
				cboTableName.Text = "Customers";
			}

			_isLoading = false;

			lblResultCount.Text = string.Empty;
			lblVersion.Text = string.Format("Version: {0}  ({1})  {2}", AssemblyExtender.DisplayVersion(), AssemblyExtender.BuildDate().ToShortDateString(), AssemblyExtender.AssemblyCompany());
		}

		private void SaveSettings()
		{
			Settings.Default.FileType = FileType.ToString();
			Settings.Default.FilePath = txtFilePath.Text;
			Settings.Default.Delimiter = Delimiter.ToString();
			Settings.Default.SheetName = cboSheetName.Text;
			Settings.Default.ConnectionString = cboConnection.SelectedValue.ToString();
			Settings.Default.TableName = cboTableName.Text;
			//Settings.Default.SplitterSize = mySplitPanel.SplitterDistance;
			Settings.Default.Save();
		}

		private string GetDelimiter()
		{
			switch (Delimiter)
			{
				case DelimiterType.Comma:
					return ",";
				case DelimiterType.Semicolon:
					return ";";
				case DelimiterType.Tab:
					return "\t";
				default:
					throw new Exception(string.Format("DelimiterType='{0}' is not handled", Delimiter));
			}
		}

		private void CopyToClipboard()
		{
			Clipboard.SetText(txtResult.Text);
		}

		#endregion



		#region Event handlers

		private void frmMain_Load(object sender, EventArgs e)
		{
			Initialize();
		}

		private void mySplitPanel_SplitterMoved(object sender, SplitterEventArgs e)
		{
			SaveSettings();
		}

		private void FileType_CheckedChanged(object sender, EventArgs e)
		{
			SetShowState();
		}

		private void chkSkipFirstLine_CheckedChanged(object sender, EventArgs e)
		{
			SetShowState();
		}

		private void cboConnection_SelectedIndexChanged(object sender, EventArgs e)
		{
			LoadTables();
		}

		void columnMap_IsModified(object sender, EventArgs e)
		{
			SetShowState();
		}

		private void txtPath_TextChanged(object sender, EventArgs e)
		{
			LoadSheetNames();
			SetShowState();
		}

		private void btnSearchFile_Click(object sender, EventArgs e)
		{
			PickFile();
		}

		private void btnParseColumns_Click(object sender, EventArgs e)
		{
			ParseSqlColumns();
		}

		private void btnGenerateInsert_Click(object sender, EventArgs e)
		{
			Generate(CommandType.Insert);
		}

		private void btnGenerateUpdate_Click(object sender, EventArgs e)
		{
			Generate(CommandType.Update);
		}

		private void btnGenerateDelete_Click(object sender, EventArgs e)
		{
			Generate(CommandType.Delete);
		}

		private void btnCopy_Click(object sender, EventArgs e)
		{
			CopyToClipboard();
		}

		#endregion
	}
}