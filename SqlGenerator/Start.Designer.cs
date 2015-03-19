namespace SqlGenerator
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
			this.cboTableName = new System.Windows.Forms.ComboBox();
			this.cboConnection = new System.Windows.Forms.ComboBox();
			this.lblTableName = new System.Windows.Forms.Label();
			this.lblConnection = new System.Windows.Forms.Label();
			this.btnGenerateDelete = new System.Windows.Forms.Button();
			this.btnGenerateUpdate = new System.Windows.Forms.Button();
			this.btnGenerateInsert = new System.Windows.Forms.Button();
			this.flowColumns = new System.Windows.Forms.FlowLayoutPanel();
			this.grpInput = new System.Windows.Forms.GroupBox();
			this.grpColumnMap = new System.Windows.Forms.GroupBox();
			this.grpOutput.SuspendLayout();
			this.grpInput.SuspendLayout();
			this.grpColumnMap.SuspendLayout();
			this.SuspendLayout();
			// 
			// txtFilePath
			// 
			this.txtFilePath.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtFilePath.BackColor = System.Drawing.Color.PapayaWhip;
			this.txtFilePath.Location = new System.Drawing.Point(182, 18);
			this.txtFilePath.Name = "txtFilePath";
			this.txtFilePath.ReadOnly = true;
			this.txtFilePath.Size = new System.Drawing.Size(571, 20);
			this.txtFilePath.TabIndex = 3;
			this.txtFilePath.Text = "C:\\Temp\\MyExcelFile.xlsx";
			this.txtFilePath.TextChanged += new System.EventHandler(this.txtPath_TextChanged);
			// 
			// lblFilePath
			// 
			this.lblFilePath.AutoSize = true;
			this.lblFilePath.Location = new System.Drawing.Point(150, 21);
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
			this.txtResult.Location = new System.Drawing.Point(12, 402);
			this.txtResult.Name = "txtResult";
			this.txtResult.ShowLineNumbers = false;
			this.txtResult.Size = new System.Drawing.Size(1206, 276);
			this.txtResult.TabIndex = 10;
			this.txtResult.VRulerRow = 0;
			// 
			// cboSheetName
			// 
			this.cboSheetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboSheetName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboSheetName.FormattingEnabled = true;
			this.cboSheetName.Location = new System.Drawing.Point(863, 18);
			this.cboSheetName.Name = "cboSheetName";
			this.cboSheetName.Size = new System.Drawing.Size(125, 21);
			this.cboSheetName.TabIndex = 5;
			// 
			// lblSheetName
			// 
			this.lblSheetName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblSheetName.AutoSize = true;
			this.lblSheetName.Location = new System.Drawing.Point(819, 21);
			this.lblSheetName.Name = "lblSheetName";
			this.lblSheetName.Size = new System.Drawing.Size(38, 13);
			this.lblSheetName.TabIndex = 8;
			this.lblSheetName.Text = "Sheet:";
			this.lblSheetName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnSearchFile
			// 
			this.btnSearchFile.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearchFile.Location = new System.Drawing.Point(759, 16);
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
			this.btnCopy.Location = new System.Drawing.Point(1125, 689);
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
			this.btnParseColumns.Location = new System.Drawing.Point(1072, 17);
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
			this.lblResultCount.Location = new System.Drawing.Point(859, 690);
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
			this.optExcel.CheckedChanged += new System.EventHandler(this.FileType_CheckedChanged);
			// 
			// optText
			// 
			this.optText.AutoSize = true;
			this.optText.Location = new System.Drawing.Point(78, 19);
			this.optText.Name = "optText";
			this.optText.Size = new System.Drawing.Size(46, 17);
			this.optText.TabIndex = 1;
			this.optText.Text = "Text";
			this.optText.UseVisualStyleBackColor = true;
			this.optText.CheckedChanged += new System.EventHandler(this.FileType_CheckedChanged);
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
			this.cboDelimiter.Location = new System.Drawing.Point(863, 18);
			this.cboDelimiter.Name = "cboDelimiter";
			this.cboDelimiter.Size = new System.Drawing.Size(125, 21);
			this.cboDelimiter.TabIndex = 5;
			this.cboDelimiter.Visible = false;
			// 
			// lblDelimiter
			// 
			this.lblDelimiter.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDelimiter.AutoSize = true;
			this.lblDelimiter.Location = new System.Drawing.Point(807, 21);
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
			this.chkFirstLineHoldsColumnNames.Location = new System.Drawing.Point(1011, 20);
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
			this.lblGenerateSql.Location = new System.Drawing.Point(789, 369);
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
			this.grpOutput.Controls.Add(this.cboTableName);
			this.grpOutput.Controls.Add(this.cboConnection);
			this.grpOutput.Controls.Add(this.btnParseColumns);
			this.grpOutput.Controls.Add(this.lblTableName);
			this.grpOutput.Controls.Add(this.lblConnection);
			this.grpOutput.Location = new System.Drawing.Point(12, 68);
			this.grpOutput.Name = "grpOutput";
			this.grpOutput.Size = new System.Drawing.Size(1206, 50);
			this.grpOutput.TabIndex = 28;
			this.grpOutput.TabStop = false;
			this.grpOutput.Text = "Target table in SQL database";
			// 
			// cboTableName
			// 
			this.cboTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.cboTableName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboTableName.FormattingEnabled = true;
			this.cboTableName.Location = new System.Drawing.Point(863, 19);
			this.cboTableName.MaxDropDownItems = 50;
			this.cboTableName.Name = "cboTableName";
			this.cboTableName.Size = new System.Drawing.Size(188, 21);
			this.cboTableName.TabIndex = 21;
			// 
			// cboConnection
			// 
			this.cboConnection.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboConnection.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboConnection.FormattingEnabled = true;
			this.cboConnection.Items.AddRange(new object[] {
            "Semicolon",
            "Comma",
            "Tab"});
			this.cboConnection.Location = new System.Drawing.Point(182, 19);
			this.cboConnection.Name = "cboConnection";
			this.cboConnection.Size = new System.Drawing.Size(571, 21);
			this.cboConnection.TabIndex = 21;
			this.cboConnection.SelectedIndexChanged += new System.EventHandler(this.cboConnection_SelectedIndexChanged);
			// 
			// lblTableName
			// 
			this.lblTableName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblTableName.AutoSize = true;
			this.lblTableName.Location = new System.Drawing.Point(792, 22);
			this.lblTableName.Name = "lblTableName";
			this.lblTableName.Size = new System.Drawing.Size(65, 13);
			this.lblTableName.TabIndex = 23;
			this.lblTableName.Text = "TableName:";
			this.lblTableName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// lblConnection
			// 
			this.lblConnection.AutoSize = true;
			this.lblConnection.Location = new System.Drawing.Point(85, 22);
			this.lblConnection.Name = "lblConnection";
			this.lblConnection.Size = new System.Drawing.Size(91, 13);
			this.lblConnection.TabIndex = 21;
			this.lblConnection.Text = "ConnectionString:";
			this.lblConnection.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// btnGenerateDelete
			// 
			this.btnGenerateDelete.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnGenerateDelete.Location = new System.Drawing.Point(1125, 368);
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
			this.btnGenerateUpdate.Location = new System.Drawing.Point(1044, 368);
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
			this.btnGenerateInsert.Location = new System.Drawing.Point(963, 368);
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
			this.flowColumns.Size = new System.Drawing.Size(1200, 219);
			this.flowColumns.TabIndex = 30;
			// 
			// grpInput
			// 
			this.grpInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpInput.Controls.Add(this.optExcel);
			this.grpInput.Controls.Add(this.optText);
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
			this.grpInput.Size = new System.Drawing.Size(1206, 50);
			this.grpInput.TabIndex = 0;
			this.grpInput.TabStop = false;
			this.grpInput.Text = "Input File";
			// 
			// grpColumnMap
			// 
			this.grpColumnMap.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.grpColumnMap.Controls.Add(this.flowColumns);
			this.grpColumnMap.Location = new System.Drawing.Point(12, 124);
			this.grpColumnMap.Name = "grpColumnMap";
			this.grpColumnMap.Size = new System.Drawing.Size(1206, 238);
			this.grpColumnMap.TabIndex = 0;
			this.grpColumnMap.TabStop = false;
			this.grpColumnMap.Text = "Map incoming data to table columns by making appropriate choices";
			// 
			// Start
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.PapayaWhip;
			this.ClientSize = new System.Drawing.Size(1230, 721);
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
			this.MinimumSize = new System.Drawing.Size(837, 646);
			this.Name = "Start";
			this.Text = "Generate SQL statements from values in file";
			this.Load += new System.EventHandler(this.frmMain_Load);
			this.grpOutput.ResumeLayout(false);
			this.grpOutput.PerformLayout();
			this.grpInput.ResumeLayout(false);
			this.grpInput.PerformLayout();
			this.grpColumnMap.ResumeLayout(false);
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
		private System.Windows.Forms.Label lblTableName;
		private System.Windows.Forms.Label lblConnection;
		private System.Windows.Forms.Button btnParseColumns;
		private System.Windows.Forms.Label lblGenerateSql;
		private System.Windows.Forms.Button btnGenerateDelete;
		private System.Windows.Forms.Button btnGenerateUpdate;
		private System.Windows.Forms.Button btnGenerateInsert;
		private System.Windows.Forms.FlowLayoutPanel flowColumns;
		private System.Windows.Forms.GroupBox grpInput;
		private System.Windows.Forms.GroupBox grpColumnMap;
		private System.Windows.Forms.ComboBox cboTableName;
		private System.Windows.Forms.ComboBox cboConnection;
	}
}

