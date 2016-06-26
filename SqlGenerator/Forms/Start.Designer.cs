namespace SqlGenerator.Forms
{
	partial class Start
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose(bool disposing)
		{
			if (disposing && (components != null))
			{
				components.Dispose();
			}
			base.Dispose(disposing);
		}

		public Start()
		{
			InitializeComponent();
		}


		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Start));
			this.txtFilePath = new System.Windows.Forms.TextBox();
			this.lblFilePath = new System.Windows.Forms.Label();
			this.txtResult = new ICSharpCode.TextEditor.TextEditorControl();
			this.cboSheetName = new System.Windows.Forms.ComboBox();
			this.lblSheetName = new System.Windows.Forms.Label();
			this.btnSearchFile = new System.Windows.Forms.Button();
			this.myToolTip = new System.Windows.Forms.ToolTip(this.components);
			this.btnCopy = new System.Windows.Forms.Button();
			this.btnParseColumns = new System.Windows.Forms.Button();
			this.lblResultCount = new System.Windows.Forms.Label();
			this.optExcel = new System.Windows.Forms.RadioButton();
			this.optText = new System.Windows.Forms.RadioButton();
			this.cboDelimiter = new System.Windows.Forms.ComboBox();
			this.lblDelimiter = new System.Windows.Forms.Label();
			this.chkFirstLineHoldsColumnNames = new System.Windows.Forms.CheckBox();
			this.lblVersion = new System.Windows.Forms.Label();
			this.lblGenerateSql = new System.Windows.Forms.Label();
			this.grpOutput = new System.Windows.Forms.GroupBox();
			this.cboTableNameTarget = new System.Windows.Forms.ComboBox();
			this.cboConnectionTarget = new System.Windows.Forms.ComboBox();
			this.lblTableNameTarget = new System.Windows.Forms.Label();
			this.lblConnectionTarget = new System.Windows.Forms.Label();
			this.btnGenerateDelete = new System.Windows.Forms.Button();
			this.btnGenerateUpdate = new System.Windows.Forms.Button();
			this.btnGenerateInsert = new System.Windows.Forms.Button();
			this.flowColumns = new System.Windows.Forms.FlowLayoutPanel();
			this.grpInput = new System.Windows.Forms.GroupBox();
			this.lblFilter = new System.Windows.Forms.Label();
			this.txtFilter = new System.Windows.Forms.TextBox();
			this.optSQL = new System.Windows.Forms.RadioButton();
			this.cboTableNameSource = new System.Windows.Forms.ComboBox();
			this.cboConnectionSource = new System.Windows.Forms.ComboBox();
			this.lblTableNameSource = new System.Windows.Forms.Label();
			this.lblConnectionSource = new System.Windows.Forms.Label();
			this.grpColumnMap = new System.Windows.Forms.GroupBox();
			this.lnkAdd = new System.Windows.Forms.LinkLabel();
			this.lnkRemove = new System.Windows.Forms.LinkLabel();
			this.lnkEdit = new System.Windows.Forms.LinkLabel();
			this.bindingConnectionSource = new System.Windows.Forms.BindingSource(this.components);
			this.bindingConnectionTarget = new System.Windows.Forms.BindingSource(this.components);
			this.grpOutput.SuspendLayout();
			this.grpInput.SuspendLayout();
			this.grpColumnMap.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.bindingConnectionSource)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingConnectionTarget)).BeginInit();
			this.SuspendLayout();
			// 
			// txtFilePath
			// 
			this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilePath.BackColor = System.Drawing.Color.PapayaWhip;
			this.txtFilePath.Location = new System.Drawing.Point(216, 19);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.ReadOnly = true;
			this.txtFilePath.Size = new System.Drawing.Size(333, 20);
			this.txtFilePath.TabIndex = 3;
			this.txtFilePath.Text = "C:\\Temp\\MyExcelFile.xlsx";
			this.txtFilePath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			// 
			// lblFilePath
			// 
			this.lblFilePath.AutoSize = true;
			this.lblFilePath.Location = new System.Drawing.Point(184, 23);
			this.lblFilePath.Name = "lblFilePath";
			this.lblFilePath.Size = new System.Drawing.Size(26, 13);
			this.lblFilePath.TabIndex = 7;
			this.lblFilePath.Text = "File:";
			this.lblFilePath.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// txtResult
			// 
			this.txtResult.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtResult.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
			this.txtResult.Cursor = System.Windows.Forms.Cursors.Default;
			this.txtResult.EnableFolding = false;
			this.txtResult.IsReadOnly = false;
			this.txtResult.Location = new System.Drawing.Point(12, 456);
			this.txtResult.Name = "txtResult";
			this.txtResult.ShowLineNumbers = false;
			this.txtResult.Size = new System.Drawing.Size(1212, 222);
			this.txtResult.TabIndex = 10;
			this.txtResult.VRulerRow = 0;
			// 
			// cboSheetName
			// 
			this.cboSheetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboSheetName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSheetName.FormattingEnabled = true;
			this.cboSheetName.Location = new System.Drawing.Point(651, 19);
			this.cboSheetName.Name = "cboSheetName";
			this.cboSheetName.Size = new System.Drawing.Size(223, 21);
			this.cboSheetName.TabIndex = 5;
			// 
			// lblSheetName
			// 
			this.lblSheetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSheetName.AutoSize = true;
			this.lblSheetName.Location = new System.Drawing.Point(607, 22);
			this.lblSheetName.Name = "lblSheetName";
			this.lblSheetName.Size = new System.Drawing.Size(38, 13);
			this.lblSheetName.TabIndex = 8;
			this.lblSheetName.Text = "Sheet:";
			this.lblSheetName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSearchFile
			// 
			this.btnSearchFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearchFile.Location = new System.Drawing.Point(555, 18);
			this.btnSearchFile.Name = "btnSearchFile";
			this.btnSearchFile.Size = new System.Drawing.Size(30, 23);
			this.btnSearchFile.TabIndex = 4;
			this.btnSearchFile.Text = "...";
			this.myToolTip.SetToolTip(this.btnSearchFile, "Select file");
			this.btnSearchFile.UseVisualStyleBackColor = true;
			this.btnSearchFile.Click += new System.EventHandler(this.btnSearchFile_Click);
			// 
			// btnCopy
			// 
			this.btnCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCopy.Location = new System.Drawing.Point(1131, 689);
			this.btnCopy.Name = "btnCopy";
			this.btnCopy.Size = new System.Drawing.Size(75, 23);
			this.btnCopy.TabIndex = 11;
			this.btnCopy.Text = "Copy";
			this.myToolTip.SetToolTip(this.btnCopy, "Copy result to Windows clipboard");
			this.btnCopy.UseVisualStyleBackColor = true;
			this.btnCopy.Click += new System.EventHandler(this.btnCopy_Click);
			// 
			// btnParseColumns
			// 
			this.btnParseColumns.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnParseColumns.Location = new System.Drawing.Point(923, 19);
			this.btnParseColumns.Name = "btnParseColumns";
			this.btnParseColumns.Size = new System.Drawing.Size(116, 23);
			this.btnParseColumns.TabIndex = 11;
			this.btnParseColumns.Text = "Parse Table Columns";
			this.myToolTip.SetToolTip(this.btnParseColumns, "Generate SQL from specified input");
			this.btnParseColumns.UseVisualStyleBackColor = true;
			this.btnParseColumns.Click += new System.EventHandler(this.btnParseColumns_Click);
			// 
			// lblResultCount
			// 
			this.lblResultCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
			this.lblResultCount.ForeColor = System.Drawing.Color.Navy;
			this.lblResultCount.Location = new System.Drawing.Point(865, 690);
			this.lblResultCount.Name = "lblResultCount";
			this.lblResultCount.Size = new System.Drawing.Size(261, 21);
			this.lblResultCount.TabIndex = 15;
			this.lblResultCount.Text = "(No result)";
			this.lblResultCount.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// optExcel
			// 
			this.optExcel.AutoSize = true;
			this.optExcel.Checked = true;
			this.optExcel.Location = new System.Drawing.Point(21, 19);
			this.optExcel.Name = "optExcel";
			this.optExcel.Size = new System.Drawing.Size(51, 17);
			this.optExcel.TabIndex = 0;
			this.optExcel.TabStop = true;
			this.optExcel.Text = "Excel";
			this.optExcel.UseVisualStyleBackColor = true;
			this.optExcel.CheckedChanged += new System.EventHandler(this.SourceType_CheckedChanged);
			// 
			// optText
			// 
			this.optText.AutoSize = true;
			this.optText.Location = new System.Drawing.Point(21, 43);
			this.optText.Name = "optText";
			this.optText.Size = new System.Drawing.Size(46, 17);
			this.optText.TabIndex = 1;
			this.optText.Text = "Text";
			this.optText.UseVisualStyleBackColor = true;
			this.optText.CheckedChanged += new System.EventHandler(this.SourceType_CheckedChanged);
			// 
			// cboDelimiter
			// 
			this.cboDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboDelimiter.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboDelimiter.FormattingEnabled = true;
			this.cboDelimiter.Items.AddRange(new object[] {
            "Semicolon",
            "Comma",
            "Tab"});
			this.cboDelimiter.Location = new System.Drawing.Point(651, 19);
			this.cboDelimiter.Name = "cboDelimiter";
			this.cboDelimiter.Size = new System.Drawing.Size(223, 21);
			this.cboDelimiter.TabIndex = 5;
			this.cboDelimiter.Visible = false;
			// 
			// lblDelimiter
			// 
			this.lblDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDelimiter.AutoSize = true;
			this.lblDelimiter.Location = new System.Drawing.Point(595, 22);
			this.lblDelimiter.Name = "lblDelimiter";
			this.lblDelimiter.Size = new System.Drawing.Size(50, 13);
			this.lblDelimiter.TabIndex = 20;
			this.lblDelimiter.Text = "Delimiter:";
			this.lblDelimiter.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			this.lblDelimiter.Visible = false;
			// 
			// chkFirstLineHoldsColumnNames
			// 
			this.chkFirstLineHoldsColumnNames.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.chkFirstLineHoldsColumnNames.AutoSize = true;
			this.chkFirstLineHoldsColumnNames.Checked = true;
			this.chkFirstLineHoldsColumnNames.CheckState = System.Windows.Forms.CheckState.Checked;
			this.chkFirstLineHoldsColumnNames.Location = new System.Drawing.Point(1017, 21);
			this.chkFirstLineHoldsColumnNames.Name = "chkFirstLineHoldsColumnNames";
			this.chkFirstLineHoldsColumnNames.Size = new System.Drawing.Size(193, 17);
			this.chkFirstLineHoldsColumnNames.TabIndex = 2;
			this.chkFirstLineHoldsColumnNames.Text = "Skip first line holding column names";
			this.chkFirstLineHoldsColumnNames.UseVisualStyleBackColor = true;
			this.chkFirstLineHoldsColumnNames.CheckedChanged += new System.EventHandler(this.chkSkipFirstLine_CheckedChanged);
			// 
			// lblVersion
			// 
			this.lblVersion.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
			this.lblVersion.ForeColor = System.Drawing.Color.Navy;
			this.lblVersion.Location = new System.Drawing.Point(17, 690);
			this.lblVersion.Name = "lblVersion";
			this.lblVersion.Size = new System.Drawing.Size(307, 21);
			this.lblVersion.TabIndex = 22;
			this.lblVersion.Text = "Version. 1.0 (2014-01-01)";
			this.lblVersion.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblGenerateSql
			// 
			this.lblGenerateSql.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblGenerateSql.Location = new System.Drawing.Point(795, 418);
			this.lblGenerateSql.Name = "lblGenerateSql";
			this.lblGenerateSql.Size = new System.Drawing.Size(168, 21);
			this.lblGenerateSql.TabIndex = 13;
			this.lblGenerateSql.Text = "Generate SQL statements:";
			this.lblGenerateSql.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// grpOutput
			// 
			this.grpOutput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpOutput.Controls.Add(this.lnkEdit);
			this.grpOutput.Controls.Add(this.cboTableNameTarget);
			this.grpOutput.Controls.Add(this.lnkRemove);
			this.grpOutput.Controls.Add(this.lnkAdd);
			this.grpOutput.Controls.Add(this.cboConnectionTarget);
			this.grpOutput.Controls.Add(this.btnParseColumns);
			this.grpOutput.Controls.Add(this.lblTableNameTarget);
			this.grpOutput.Controls.Add(this.lblConnectionTarget);
			this.grpOutput.Location = new System.Drawing.Point(12, 117);
			this.grpOutput.Name = "grpOutput";
			this.grpOutput.Size = new System.Drawing.Size(1212, 50);
			this.grpOutput.TabIndex = 28;
			this.grpOutput.TabStop = false;
			this.grpOutput.Text = "Target table in SQL database";
			// 
			// cboTableNameTarget
			// 
			this.cboTableNameTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTableNameTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTableNameTarget.DropDownWidth = 223;
			this.cboTableNameTarget.FormattingEnabled = true;
			this.cboTableNameTarget.Location = new System.Drawing.Point(651, 19);
			this.cboTableNameTarget.MaxDropDownItems = 50;
			this.cboTableNameTarget.Name = "cboTableNameTarget";
			this.cboTableNameTarget.Size = new System.Drawing.Size(223, 21);
			this.cboTableNameTarget.TabIndex = 21;
			// 
			// cboConnectionTarget
			// 
			this.cboConnectionTarget.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboConnectionTarget.DisplayMember = "DisplayName";
			this.cboConnectionTarget.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboConnectionTarget.FormattingEnabled = true;
			this.cboConnectionTarget.Items.AddRange(new object[] {
            "Semicolon",
            "Comma",
            "Tab"});
			this.cboConnectionTarget.Location = new System.Drawing.Point(216, 19);
			this.cboConnectionTarget.Name = "cboConnectionTarget";
			this.cboConnectionTarget.Size = new System.Drawing.Size(333, 21);
			this.cboConnectionTarget.TabIndex = 21;
			this.cboConnectionTarget.ValueMember = "ConnectionString";
			this.cboConnectionTarget.SelectedIndexChanged += new System.EventHandler(this.cboConnectionTarget_SelectedIndexChanged);
			// 
			// lblTableNameTarget
			// 
			this.lblTableNameTarget.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTableNameTarget.AutoSize = true;
			this.lblTableNameTarget.Location = new System.Drawing.Point(580, 22);
			this.lblTableNameTarget.Name = "lblTableNameTarget";
			this.lblTableNameTarget.Size = new System.Drawing.Size(65, 13);
			this.lblTableNameTarget.TabIndex = 23;
			this.lblTableNameTarget.Text = "TableName:";
			this.lblTableNameTarget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblConnectionTarget
			// 
			this.lblConnectionTarget.AutoSize = true;
			this.lblConnectionTarget.Location = new System.Drawing.Point(125, 22);
			this.lblConnectionTarget.Name = "lblConnectionTarget";
			this.lblConnectionTarget.Size = new System.Drawing.Size(91, 13);
			this.lblConnectionTarget.TabIndex = 21;
			this.lblConnectionTarget.Text = "ConnectionString:";
			this.lblConnectionTarget.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnGenerateDelete
			// 
			this.btnGenerateDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGenerateDelete.Location = new System.Drawing.Point(1131, 417);
			this.btnGenerateDelete.Name = "btnGenerateDelete";
			this.btnGenerateDelete.Size = new System.Drawing.Size(75, 23);
			this.btnGenerateDelete.TabIndex = 12;
			this.btnGenerateDelete.Text = "Delete";
			this.btnGenerateDelete.UseVisualStyleBackColor = true;
			this.btnGenerateDelete.Click += new System.EventHandler(this.btnGenerateDelete_Click);
			// 
			// btnGenerateUpdate
			// 
			this.btnGenerateUpdate.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGenerateUpdate.Location = new System.Drawing.Point(1050, 417);
			this.btnGenerateUpdate.Name = "btnGenerateUpdate";
			this.btnGenerateUpdate.Size = new System.Drawing.Size(75, 23);
			this.btnGenerateUpdate.TabIndex = 11;
			this.btnGenerateUpdate.Text = "Update";
			this.btnGenerateUpdate.UseVisualStyleBackColor = true;
			this.btnGenerateUpdate.Click += new System.EventHandler(this.btnGenerateUpdate_Click);
			// 
			// btnGenerateInsert
			// 
			this.btnGenerateInsert.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGenerateInsert.Location = new System.Drawing.Point(969, 417);
			this.btnGenerateInsert.Name = "btnGenerateInsert";
			this.btnGenerateInsert.Size = new System.Drawing.Size(75, 23);
			this.btnGenerateInsert.TabIndex = 9;
			this.btnGenerateInsert.Text = "Insert";
			this.btnGenerateInsert.UseVisualStyleBackColor = true;
			this.btnGenerateInsert.Click += new System.EventHandler(this.btnGenerateInsert_Click);
			// 
			// flowColumns
			// 
			this.flowColumns.AutoScroll = true;
			this.flowColumns.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flowColumns.Location = new System.Drawing.Point(3, 16);
			this.flowColumns.Name = "flowColumns";
			this.flowColumns.Size = new System.Drawing.Size(1206, 219);
			this.flowColumns.TabIndex = 30;
			// 
			// grpInput
			// 
			this.grpInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpInput.Controls.Add(this.lblFilter);
			this.grpInput.Controls.Add(this.txtFilter);
			this.grpInput.Controls.Add(this.optSQL);
			this.grpInput.Controls.Add(this.cboTableNameSource);
			this.grpInput.Controls.Add(this.optExcel);
			this.grpInput.Controls.Add(this.cboConnectionSource);
			this.grpInput.Controls.Add(this.lblTableNameSource);
			this.grpInput.Controls.Add(this.optText);
			this.grpInput.Controls.Add(this.lblConnectionSource);
			this.grpInput.Controls.Add(this.lblFilePath);
			this.grpInput.Controls.Add(this.txtFilePath);
			this.grpInput.Controls.Add(this.lblDelimiter);
			this.grpInput.Controls.Add(this.chkFirstLineHoldsColumnNames);
			this.grpInput.Controls.Add(this.lblSheetName);
			this.grpInput.Controls.Add(this.btnSearchFile);
			this.grpInput.Controls.Add(this.cboDelimiter);
			this.grpInput.Controls.Add(this.cboSheetName);
			this.grpInput.Location = new System.Drawing.Point(12, 12);
			this.grpInput.Name = "grpInput";
			this.grpInput.Size = new System.Drawing.Size(1212, 99);
			this.grpInput.TabIndex = 0;
			this.grpInput.TabStop = false;
			this.grpInput.Text = "Input Source";
			// 
			// lblFilter
			// 
			this.lblFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblFilter.AutoSize = true;
			this.lblFilter.Location = new System.Drawing.Point(887, 69);
			this.lblFilter.Name = "lblFilter";
			this.lblFilter.Size = new System.Drawing.Size(32, 13);
			this.lblFilter.TabIndex = 30;
			this.lblFilter.Text = "Filter:";
			this.lblFilter.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
			// 
			// txtFilter
			// 
			this.txtFilter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilter.Location = new System.Drawing.Point(923, 66);
			this.txtFilter.Name = "txtFilter";
			this.txtFilter.Size = new System.Drawing.Size(271, 20);
			this.txtFilter.TabIndex = 29;
			// 
			// optSQL
			// 
			this.optSQL.AutoSize = true;
			this.optSQL.Location = new System.Drawing.Point(21, 66);
			this.optSQL.Name = "optSQL";
			this.optSQL.Size = new System.Drawing.Size(46, 17);
			this.optSQL.TabIndex = 28;
			this.optSQL.Text = "SQL";
			this.optSQL.UseVisualStyleBackColor = true;
			this.optSQL.CheckedChanged += new System.EventHandler(this.SourceType_CheckedChanged);
			// 
			// cboTableNameSource
			// 
			this.cboTableNameSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTableNameSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTableNameSource.DropDownWidth = 223;
			this.cboTableNameSource.FormattingEnabled = true;
			this.cboTableNameSource.Location = new System.Drawing.Point(651, 65);
			this.cboTableNameSource.MaxDropDownItems = 50;
			this.cboTableNameSource.Name = "cboTableNameSource";
			this.cboTableNameSource.Size = new System.Drawing.Size(223, 21);
			this.cboTableNameSource.TabIndex = 24;
			// 
			// cboConnectionSource
			// 
			this.cboConnectionSource.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboConnectionSource.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboConnectionSource.FormattingEnabled = true;
			this.cboConnectionSource.Items.AddRange(new object[] {
            "Semicolon",
            "Comma",
            "Tab"});
			this.cboConnectionSource.Location = new System.Drawing.Point(216, 65);
			this.cboConnectionSource.Name = "cboConnectionSource";
			this.cboConnectionSource.Size = new System.Drawing.Size(333, 21);
			this.cboConnectionSource.TabIndex = 25;
			this.cboConnectionSource.SelectedIndexChanged += new System.EventHandler(this.cboConnectionSource_SelectedIndexChanged);
			// 
			// lblTableNameSource
			// 
			this.lblTableNameSource.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTableNameSource.AutoSize = true;
			this.lblTableNameSource.Location = new System.Drawing.Point(580, 68);
			this.lblTableNameSource.Name = "lblTableNameSource";
			this.lblTableNameSource.Size = new System.Drawing.Size(65, 13);
			this.lblTableNameSource.TabIndex = 27;
			this.lblTableNameSource.Text = "TableName:";
			this.lblTableNameSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblConnectionSource
			// 
			this.lblConnectionSource.AutoSize = true;
			this.lblConnectionSource.Location = new System.Drawing.Point(125, 69);
			this.lblConnectionSource.Name = "lblConnectionSource";
			this.lblConnectionSource.Size = new System.Drawing.Size(91, 13);
			this.lblConnectionSource.TabIndex = 26;
			this.lblConnectionSource.Text = "ConnectionString:";
			this.lblConnectionSource.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// grpColumnMap
			// 
			this.grpColumnMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpColumnMap.Controls.Add(this.flowColumns);
			this.grpColumnMap.Location = new System.Drawing.Point(12, 173);
			this.grpColumnMap.Name = "grpColumnMap";
			this.grpColumnMap.Size = new System.Drawing.Size(1212, 238);
			this.grpColumnMap.TabIndex = 0;
			this.grpColumnMap.TabStop = false;
			this.grpColumnMap.Text = "Map incoming data to table columns by making appropriate choices";
			// 
			// lnkAdd
			// 
			this.lnkAdd.AutoSize = true;
			this.lnkAdd.Location = new System.Drawing.Point(9, 22);
			this.lnkAdd.Name = "lnkAdd";
			this.lnkAdd.Size = new System.Drawing.Size(26, 13);
			this.lnkAdd.TabIndex = 44;
			this.lnkAdd.TabStop = true;
			this.lnkAdd.Text = "Add";
			this.myToolTip.SetToolTip(this.lnkAdd, "Add new connection");
			this.lnkAdd.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkAdd_LinkClicked);
			// 
			// lnkRemove
			// 
			this.lnkRemove.AutoSize = true;
			this.lnkRemove.Location = new System.Drawing.Point(72, 22);
			this.lnkRemove.Name = "lnkRemove";
			this.lnkRemove.Size = new System.Drawing.Size(47, 13);
			this.lnkRemove.TabIndex = 45;
			this.lnkRemove.TabStop = true;
			this.lnkRemove.Text = "Remove";
			this.myToolTip.SetToolTip(this.lnkRemove, "Remove current connection");
			this.lnkRemove.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkRemove_LinkClicked);
			// 
			// lnkEdit
			// 
			this.lnkEdit.AutoSize = true;
			this.lnkEdit.Location = new System.Drawing.Point(41, 22);
			this.lnkEdit.Name = "lnkEdit";
			this.lnkEdit.Size = new System.Drawing.Size(25, 13);
			this.lnkEdit.TabIndex = 46;
			this.lnkEdit.TabStop = true;
			this.lnkEdit.Text = "Edit";
			this.myToolTip.SetToolTip(this.lnkEdit, "dit current connection");
			this.lnkEdit.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.lnkEdit_LinkClicked);
			// 
			// bindingConnectionSource
			// 
			this.bindingConnectionSource.DataSource = typeof(SqlGenerator.Entities.ConnectionItem);
			// 
			// bindingConnectionTarget
			// 
			this.bindingConnectionTarget.DataSource = typeof(SqlGenerator.Entities.ConnectionItem);
			// 
			// Start
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.PapayaWhip;
			this.ClientSize = new System.Drawing.Size(1236, 721);
			this.Controls.Add(this.grpInput);
			this.Controls.Add(this.grpColumnMap);
			this.Controls.Add(this.btnGenerateDelete);
			this.Controls.Add(this.lblGenerateSql);
			this.Controls.Add(this.btnGenerateUpdate);
			this.Controls.Add(this.grpOutput);
			this.Controls.Add(this.btnGenerateInsert);
			this.Controls.Add(this.txtResult);
			this.Controls.Add(this.lblVersion);
			this.Controls.Add(this.lblResultCount);
			this.Controls.Add(this.btnCopy);
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MinimumSize = new System.Drawing.Size(837, 646);
			this.Name = "Start";
			this.Text = "Generate SQL statements from values in file";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.grpOutput.ResumeLayout(false);
			this.grpOutput.PerformLayout();
			this.grpInput.ResumeLayout(false);
			this.grpInput.PerformLayout();
			this.grpColumnMap.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.bindingConnectionSource)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.bindingConnectionTarget)).EndInit();
			this.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.TextBox txtFilePath;
		private System.Windows.Forms.Label lblFilePath;
		private ICSharpCode.TextEditor.TextEditorControl txtResult;
		private System.Windows.Forms.ComboBox cboSheetName;
		private System.Windows.Forms.Label lblSheetName;
		private System.Windows.Forms.ToolTip myToolTip;
		private System.Windows.Forms.Button btnSearchFile;
		private System.Windows.Forms.Button btnCopy;
		private System.Windows.Forms.Label lblResultCount;
		private System.Windows.Forms.RadioButton optExcel;
		private System.Windows.Forms.RadioButton optText;
		private System.Windows.Forms.ComboBox cboDelimiter;
		private System.Windows.Forms.Label lblDelimiter;
		private System.Windows.Forms.CheckBox chkFirstLineHoldsColumnNames;
		private System.Windows.Forms.Label lblVersion;
		private System.Windows.Forms.GroupBox grpOutput;
		private System.Windows.Forms.Label lblTableNameTarget;
		private System.Windows.Forms.Label lblConnectionTarget;
		private System.Windows.Forms.Button btnParseColumns;
		private System.Windows.Forms.Label lblGenerateSql;
		private System.Windows.Forms.Button btnGenerateDelete;
		private System.Windows.Forms.Button btnGenerateUpdate;
		private System.Windows.Forms.Button btnGenerateInsert;
		private System.Windows.Forms.FlowLayoutPanel flowColumns;
		private System.Windows.Forms.GroupBox grpInput;
		private System.Windows.Forms.GroupBox grpColumnMap;
		private System.Windows.Forms.ComboBox cboTableNameTarget;
		private System.Windows.Forms.ComboBox cboConnectionTarget;
        private System.Windows.Forms.RadioButton optSQL;
        private System.Windows.Forms.ComboBox cboTableNameSource;
        private System.Windows.Forms.ComboBox cboConnectionSource;
        private System.Windows.Forms.Label lblTableNameSource;
        private System.Windows.Forms.Label lblConnectionSource;
        private System.Windows.Forms.TextBox txtFilter;
        private System.Windows.Forms.Label lblFilter;
		private System.Windows.Forms.BindingSource bindingConnectionSource;
		private System.Windows.Forms.LinkLabel lnkEdit;
		private System.Windows.Forms.LinkLabel lnkRemove;
		private System.Windows.Forms.LinkLabel lnkAdd;
		private System.Windows.Forms.BindingSource bindingConnectionTarget;
	}
}

