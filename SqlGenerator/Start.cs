using System;
using System.Collections.Generic;
using System.ComponentModel;
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

        private SourceType SourceType
        {
            get
            {
                if (optExcel.Checked)
                    return SourceType.Excel;
                if (optText.Checked)
                    return SourceType.Text;
                if (optSQL.Checked)
                    return SourceType.Sql;
                throw new Exception("Unknown FileType");
            }
            set
            {
                switch (value)
                {
                    case SourceType.Excel:
                        optExcel.Checked = true;
                        break;
                    case SourceType.Text:
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
                switch (SourceType)
                {
                    case SourceType.Excel:
                        title = "Select the Excel file holding values for the SQL generation";
                        defaultExt = "xlsx";
                        filter = "Excel Files|*.xlsx;*.xls|All Files (*.*)|*.*";
                        break;
                    case SourceType.Text:
                        title = "Select the text file holding values for the SQL generation";
                        defaultExt = "txt";
                        filter = "Text Files|*.txt;*.csv;*.tab|All Files (*.*)|*.*";
                        break;
                    default:
                        throw new Exception(string.Format("SourceType={0} is not handled", SourceType));
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
                    if (SourceType == SourceType.Text && dlg.FileName != null)
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
                    if (SourceType == SourceType.Excel)
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
                if (SourceType == SourceType.Excel && File.Exists(txtFilePath.Text) && !_isLoading)
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
                var sqlColumns = SqlParser.GetColumns(cboConnectionTarget.SelectedValue.ToString(), cboTableNameTarget.Text);
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
            switch (SourceType)
            {
                case SourceType.Excel:
                case SourceType.Text:
                    var rowItems = ParseDataContent(false);
                    var inputColumns = rowItems[0].Values.Select((value, index) => new FileColumn { Index = index, Name = value.ToString() }).ToList();
                    return inputColumns;
                case SourceType.Sql:
                    var sqlColumns = SqlParser.GetColumns(cboConnectionTarget.SelectedValue.ToString(), cboTableNameTarget.Text);
                    var inputColumns2 = sqlColumns.Select(s => new FileColumn { Index = s.ColumnOrdinal, Name = s.ColumnName }).ToList();
                    return inputColumns2;
                default:
                    throw new Exception(string.Format("SourceType={0} is not handled", SourceType));
            }
        }

        private void Generate(CommandType commandType)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                txtResult.Text = string.Empty;
                SaveSettings();

                var dataContent = ParseDataContent(chkFirstLineHoldsColumnNames.Checked);

                var result = ResultCreator.GetResult(commandType, dataContent, MappedColumns, cboTableNameTarget.Text);
                txtResult.Text = result;
                lblResultCount.Text = string.Format("{0} records generated at {1}", txtResult.ActiveTextAreaControl.TextArea.Document.TotalNumberOfLines, DateTime.Now.ToString("HH:mm:ss"));
                SetShowState();

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

        private List<FileRow> ParseDataContent(bool skipFirstLine)
        {
            switch (SourceType)
            {
                case SourceType.Excel:
                case SourceType.Text:
                    if (!File.Exists(txtFilePath.Text))
                    {
                        throw new Exception("Specified file does not exist");
                    }
                    break;
            }

            switch (SourceType)
            {
                case SourceType.Excel:
                    return ExcelParser.GetValues(txtFilePath.Text, cboSheetName.Text, skipFirstLine);
                case SourceType.Text:
                    return TextParser.GetValues(txtFilePath.Text, GetDelimiter(), skipFirstLine);
                case SourceType.Sql:
                    return SqlParser.GetValues(cboConnectionSource.SelectedValue.ToString(), cboTableNameSource.SelectedValue.ToString(), txtFilter.Text);
                default:
                    throw new Exception(string.Format("SourceType={0} is not handled", SourceType));
            }
        }

        private void SetShowState()
        {
            grpInput.Controls.OfType<Label>().ForEach(x => x.Visible = false);
            grpInput.Controls.OfType<TextBox>().ForEach(x => x.Visible = false);
            grpInput.Controls.OfType<ComboBox>().ForEach(x => x.Visible = false);
            grpInput.Controls.OfType<Button>().ForEach(x => x.Visible = false);
            grpInput.Controls.OfType<CheckBox>().ForEach(x => x.Visible = false);
            switch (SourceType)
            {
                case SourceType.Excel:
                    lblFilePath.Visible = true;
                    txtFilePath.Visible = true;
                    btnSearchFile.Visible = true;
                    lblSheetName.Visible = true;
                    cboSheetName.Visible = true;
                    chkFirstLineHoldsColumnNames.Visible = true;
                    btnGenerateInsert.Enabled = txtFilePath.Text.IsSpecified() && cboSheetName.Text.IsSpecified();
                    break;
                case SourceType.Text:
                    lblFilePath.Visible = true;
                    txtFilePath.Visible = true;
                    btnSearchFile.Visible = true;
                    lblDelimiter.Visible = true;
                    cboDelimiter.Visible = true;
                    chkFirstLineHoldsColumnNames.Visible = true;
                    btnGenerateInsert.Enabled = txtFilePath.Text.IsSpecified();
                    break;
                case SourceType.Sql:
                    lblConnectionSource.Visible = true;
                    cboConnectionSource.Visible = true;
                    lblTableNameSource.Visible = true;
                    cboTableNameSource.Visible = true;
                    lblFilter.Visible = true;
                    txtFilter.Visible = true;
                    btnGenerateInsert.Enabled = true;
                    break;
                default:
                    throw new Exception(string.Format("FileType={0} is not handled", SourceType));
            }
            myToolTip.SetToolTip(btnSearchFile, string.Format("Select {0} file", SourceType));

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

            cboConnectionSource.DisplayMember = "Name";
            cboConnectionSource.ValueMember = "ConnectionString";
            cboConnectionSource.DataSource = connections;

            cboConnectionTarget.DisplayMember = "Name";
            cboConnectionTarget.ValueMember = "ConnectionString";
            cboConnectionTarget.DataSource = connections;
        }

        private void LoadSourceTables()
        {
            cboTableNameSource.DataSource = SqlParser.GetTables(cboConnectionSource.SelectedValue.ToString());
        }

        private void LoadTargetTables()
        {
            cboTableNameTarget.DataSource = SqlParser.GetTables(cboConnectionTarget.SelectedValue.ToString());
        }

        private void GetSettings()
        {
            _isLoading = true;


            SourceType = Settings.Default.FileType.ToEnum<SourceType>();
            txtFilePath.Text = Settings.Default.FilePath;
            LoadSheetNames();
            Delimiter = Settings.Default.Delimiter.ToEnum<DelimiterType>();
            cboSheetName.Text = Settings.Default.SheetName;
            LoadConnections();
            cboConnectionTarget.SelectedValue = Settings.Default.ConnectionString;
            cboTableNameTarget.Text = Settings.Default.TableName;


            if (Environment.UserName == "jaklithn")
            {
                // Test data during development
                SourceType = SourceType.Excel;
                txtFilePath.Text = @"C:\Temp\Customers.xlsx";
                Delimiter = DelimiterType.Semicolon;
                cboSheetName.Text = "Customers";
                cboTableNameTarget.Text = "Customers";
            }

            _isLoading = false;

            lblResultCount.Text = string.Empty;
            lblVersion.Text = string.Format("Version: {0}  ({1})  {2}", AssemblyExtender.DisplayVersion(), AssemblyExtender.BuildDate().ToShortDateString(), AssemblyExtender.AssemblyCompany());
        }

        private void SaveSettings()
        {
            Settings.Default.FileType = SourceType.ToString();
            Settings.Default.FilePath = txtFilePath.Text;
            Settings.Default.Delimiter = Delimiter.ToString();
            Settings.Default.SheetName = cboSheetName.Text;
            Settings.Default.ConnectionString = cboConnectionTarget.SelectedValue.ToString();
            Settings.Default.TableName = cboTableNameTarget.Text;
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

        private void SourceType_CheckedChanged(object sender, EventArgs e)
        {
            SetShowState();
        }

        private void chkSkipFirstLine_CheckedChanged(object sender, EventArgs e)
        {
            SetShowState();
        }

        private void cboConnectionSource_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadSourceTables();
        }

        private void cboConnectionTarget_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadTargetTables();
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