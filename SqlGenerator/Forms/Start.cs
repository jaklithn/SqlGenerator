using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using ICSharpCode.TextEditor.Document;
using SqlGenerator.Controls;
using SqlGenerator.DomainServices;
using SqlGenerator.Entities;
using SqlGenerator.Entities.Enums;
using SqlGenerator.Extenders;


namespace SqlGenerator.Forms
{
    public partial class Start : Form
    {
        private bool _isLoading;


        #region Properties

        private ConnectionItem SelectedTargetConnection
        {
            get { return (ConnectionItem)cboConnectionTarget.SelectedItem; }
        }

        private SourceType SourceType
        {
            get
            {
                if (optExcel.Checked)
                {
                    return SourceType.Excel;
                }
                if (optText.Checked)
                {
                    return SourceType.Text;
                }
                if (optSQL.Checked)
                {
                    return SourceType.Sql;
                }
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
                    case SourceType.Sql:
                        optSQL.Checked = true;
                        break;
                    default:
                        throw new Exception($"FileType={value} is not handled");
                }
            }
        }

        private DelimiterType Delimiter
        {
            get { return cboDelimiter.Text.ToEnum<DelimiterType>(); }
            set
            {
                try
                {
                    cboDelimiter.Text = value.ToString();
                    if (cboDelimiter.Text.IsNullOrEmpty())
                    {
                        cboDelimiter.Text = DelimiterType.Semicolon.ToString();
                    }
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
            LoadSettings();
            LoadSheetNames();
            SetShowState();

            // Add SQL syntax for text control
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
                        throw new Exception($"SourceType={SourceType} is not handled");
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
                    {
                        dlg.InitialDirectory = dir;
                    }
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
                {
                    if (
                        MessageBox.Show("The specified Excel file is open.\r\nPlease close the file before you retry parsing sheet names!\r\n(Cancel will close the application)",
                            "Sheet names can not be parsed", MessageBoxButtons.RetryCancel, MessageBoxIcon.Exclamation) == DialogResult.Retry)
                    {
                        LoadSheetNames();
                    }
                    else
                    {
                        Application.Exit();
                    }
                }
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
                SaveSettings();
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
                    throw new Exception($"SourceType={SourceType} is not handled");
            }
        }

        private void Generate(CommandType commandType)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                txtResult.Text = string.Empty;
                SaveSettings();

                var fileRows = ParseDataContent(chkFirstLineHoldsColumnNames.Checked);

                var result = ResultCreator.GetResult(commandType, fileRows, MappedColumns, cboTableNameTarget.Text);
                txtResult.Text = result;
                lblResultCount.Text = $"{txtResult.ActiveTextAreaControl.TextArea.Document.TotalNumberOfLines} records generated at {DateTime.Now.ToString("HH:mm:ss")}";
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

        private void GenerateDatabase()
        {
            var skippedKeys = new[] { "dtproperties", "sandeslatt_user", "LogItem", "WebStat", "Log", "Song" };
            try
            {
                Cursor = Cursors.WaitCursor;
                txtResult.Text = string.Empty;
                SaveSettings();

                var count = 0;
                var tableNames = SqlParser.GetTablesOrderedForInsert(cboConnectionSource.SelectedValue.ToString());

                // DELETE
                txtResult.Text += "-- First clear all tables in reverse order" + Environment.NewLine;
                for (var i = tableNames.Count - 1; i >= 0; i--)
                {
                    var tableName = tableNames[i];
                    if (tableName.ContainsAny(skippedKeys))
                    {
                        txtResult.Text += $"-- Skipped {tableName}" + Environment.NewLine;
                    }
                    else
                    {
                        txtResult.Text += $"DELETE FROM {tableName}" + Environment.NewLine;
                    }
                }
                txtResult.Text += Environment.NewLine + Environment.NewLine;


                // INSERT
                foreach (var tableName in tableNames)
                {
                    var fileRows = SqlParser.GetValues(cboConnectionSource.SelectedValue.ToString(), tableName, "");
                    if (tableName.ContainsAny(skippedKeys))
                    {
                        txtResult.Text += $"-- Table {tableName} skipped" + Environment.NewLine;
                        continue;
                    }

                    // Create column structs to satisfy ResultCreator
                    var sqlColumns = SqlParser.GetColumns(cboConnectionTarget.SelectedValue.ToString(), tableName);
                    var mappedColumns = sqlColumns.Select((x, i) => new ColumnMap(x, new List<FileColumn>() { new FileColumn { Index = i, Name = x.ColumnName } })).ToList();
                    var hasIdentity = sqlColumns.Any(x => x.IsIdentity);

                    // Print result
                    var result = ResultCreator.GetResult(CommandType.Insert, fileRows, mappedColumns, tableName);
                    if (hasIdentity)
                    {
                        txtResult.Text += $"SET IDENTITY_INSERT {tableName} ON;" + Environment.NewLine;
                        txtResult.Text += result + Environment.NewLine;
                        txtResult.Text += $"SET IDENTITY_INSERT {tableName} OFF;" + Environment.NewLine + Environment.NewLine;
                    }
                    else
                    {
                        txtResult.Text += result + Environment.NewLine + Environment.NewLine;
                    }

                    count += fileRows.Count;
                }
                lblResultCount.Text = $"{count} records generated at {DateTime.Now.ToString("HH:mm:ss")}";
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
                    throw new Exception($"SourceType={SourceType} is not handled");
            }
        }

        private void SetShowState()
        {
            grpInput.Controls.OfType<Label>().ForEach(x => x.Visible = false);
            grpInput.Controls.OfType<TextBox>().ForEach(x => x.Visible = false);
            grpInput.Controls.OfType<ComboBox>().ForEach(x => x.Visible = false);
            grpInput.Controls.OfType<CheckBox>().ForEach(x => x.Visible = false);
            grpInput.Controls.OfType<Button>().ForEach(x => x.Visible = false);
            btnSearchFile.Visible = false;

            var hasValidColumns = MappedColumns.Any() && MappedColumns.All(c => c.IsValid);
            var isFullDatabaseCopy = chkFullDatabaseCopy.Visible && chkFullDatabaseCopy.Checked;

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
                    chkFullDatabaseCopy.Visible = false;
                    chkFullDatabaseCopy.Checked = false;
                    break;
                case SourceType.Text:
                    lblFilePath.Visible = true;
                    txtFilePath.Visible = true;
                    btnSearchFile.Visible = true;
                    lblDelimiter.Visible = true;
                    cboDelimiter.Visible = true;
                    chkFirstLineHoldsColumnNames.Visible = true;
                    btnGenerateInsert.Enabled = txtFilePath.Text.IsSpecified();
                    chkFullDatabaseCopy.Visible = false;
                    chkFullDatabaseCopy.Checked = false;
                    break;
                case SourceType.Sql:
                    lblConnectionSource.Visible = true;
                    cboConnectionSource.Visible = true;
                    lblTableNameSource.Visible = !isFullDatabaseCopy;
                    cboTableNameSource.Visible = !isFullDatabaseCopy;
                    lblFilter.Visible = !isFullDatabaseCopy;
                    txtFilter.Visible = !isFullDatabaseCopy;
                    btnGenerateInsert.Enabled = true;
                    chkFullDatabaseCopy.Visible = true;
                    break;
                default:
                    throw new Exception($"FileType={SourceType} is not handled");
            }
            myToolTip.SetToolTip(btnSearchFile, $"Select {SourceType} file");

            grpOutput.Visible = !isFullDatabaseCopy;
            grpColumnMap.Visible = !isFullDatabaseCopy;
            grpColumnMap.Text = $"Map incoming {(chkFirstLineHoldsColumnNames.Checked ? "columns" : "demo data")} to table columns by making appropriate choices";

            btnGenerateInsert.Enabled = hasValidColumns || isFullDatabaseCopy;
            btnGenerateUpdate.Enabled = hasValidColumns;
            btnGenerateDelete.Enabled = hasValidColumns;
            btnGenerateDatabase.Visible = isFullDatabaseCopy;
            btnGenerateInsert.Visible = !isFullDatabaseCopy;
            btnGenerateUpdate.Visible = !isFullDatabaseCopy;
            btnGenerateDelete.Visible = !isFullDatabaseCopy;

            btnCopy.Enabled = txtResult.Text.IsSpecified();
        }

        private static void LoadConnections(ComboBox comboBox, List<ConnectionItem> connectionItems)
        {
            var selectedItem = comboBox.SelectedItem;
            comboBox.DisplayMember = "DisplayName";
            comboBox.ValueMember = "ConnectionString";
            comboBox.DataSource = connectionItems;
            if (selectedItem != null)
            {
                // Reset possible selected item
                comboBox.SelectedItem = selectedItem;
            }
        }

        private void LoadSourceTables()
        {
            cboTableNameSource.DataSource = cboConnectionSource.SelectedValue == null ? null : SqlParser.GetTables(cboConnectionSource.SelectedValue.ToString());
        }

        private void LoadTargetTables()
        {
            cboTableNameTarget.DataSource = cboConnectionTarget.SelectedValue == null ? null : SqlParser.GetTables(cboConnectionTarget.SelectedValue.ToString());
        }

        private void AddConnection()
        {
            var f = new Connection();
            var dialogResult = f.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                var settings = SettingsHandler.Get();
                settings.ConnectionStrings.Add(f.ConnectionString);
                LoadConnections(cboConnectionTarget, settings.GetConnectionItems());
                SaveSettings();
                LoadSettings(false);
            }
        }

        private void EditConnection()
        {
            if (SelectedTargetConnection == null)
            {
                MessageBox.Show("There is no connection to edit", "Edit", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }

            var f = new Connection { ConnectionString = SelectedTargetConnection.ConnectionString };
            var dialogResult = f.ShowDialog();
            if (dialogResult == DialogResult.OK)
            {
                SelectedTargetConnection.ConnectionString = f.ConnectionString;
                SaveSettings();
                LoadSettings();
            }
        }

        private void RemoveConnection()
        {
            if (SelectedTargetConnection == null)
            {
                MessageBox.Show("There is no connection to remove", "Remove", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            var settings = LoadSettings();
            settings.ConnectionStrings.Remove(SelectedTargetConnection.ConnectionString);
            LoadConnections(cboConnectionTarget, settings.GetConnectionItems());
            SaveSettings();
            LoadSettings();
        }

        private Settings LoadSettings(bool includeFileSettings = true)
        {
            _isLoading = true;

            var settings = SettingsHandler.Get();

            if (includeFileSettings)
            {
                SourceType = settings.FileType.ToEnum<SourceType>();
                txtFilePath.Text = settings.FilePath;
                LoadSheetNames();
                Delimiter = settings.Delimiter.ToEnum<DelimiterType>();
                cboSheetName.Text = settings.SheetName;
            }

            LoadConnections(cboConnectionTarget, settings.GetConnectionItems());
            LoadConnections(cboConnectionSource, settings.GetConnectionItems());
            var selectedConnectionItem = settings.GetConnectionItems().SingleOrDefault(x => x.ConnectionString == settings.SelectedConnectionString);
            if (selectedConnectionItem != null)
            {
                cboConnectionSource.SelectedText = settings.SelectedConnectionString;
                cboConnectionTarget.SelectedText = settings.SelectedConnectionString;
            }
            cboConnectionTarget.SelectedValue = settings.SelectedConnectionString;
            cboTableNameTarget.Text = settings.TableName;

            _isLoading = false;

            lblResultCount.Text = string.Empty;
            lblVersion.Text = $"Version: {AssemblyExtender.DisplayVersion()}  ({AssemblyExtender.BuildDate().ToShortDateString()})  {AssemblyExtender.AssemblyCompany()}";
            return settings;
        }

        private void SaveSettings()
        {
            var connectionStrings = (List<ConnectionItem>)cboConnectionTarget.DataSource;
            var settings = new Settings
            {
                ConnectionStrings = connectionStrings.Select(x => x.ConnectionString).Distinct().ToList(),
                FileType = SourceType.ToString(),
                FilePath = txtFilePath.Text,
                Delimiter = Delimiter.ToString(),
                SheetName = cboSheetName.Text,
                SelectedConnectionString = cboConnectionTarget.SelectedValue.ToString(),
                TableName = cboTableNameTarget.Text
            };
            SettingsHandler.Save(settings);
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

        private void lnkAdd_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            AddConnection();
        }

        private void lnkEdit_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            EditConnection();
        }

        private void lnkRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            RemoveConnection();
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

        private void chkFullDatabaseCopy_CheckedChanged(object sender, EventArgs e)
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

        private void columnMap_IsModified(object sender, EventArgs e)
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

        private void btnGenerateDatabase_Click(object sender, EventArgs e)
        {
            GenerateDatabase();
        }

        private void btnCopy_Click(object sender, EventArgs e)
        {
            CopyToClipboard();
        }

        #endregion
    }
}