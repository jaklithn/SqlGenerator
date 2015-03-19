namespace SqlGenerator.Controls
{
	partial class ColumnMap
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

		#region Component Designer generated code

		/// <summary> 
		/// Required method for Designer support - do not modify 
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.lblColumnName = new System.Windows.Forms.Label();
			this.cboFileColumn = new System.Windows.Forms.ComboBox();
			this.txtStaticInput = new System.Windows.Forms.TextBox();
			this.lblDataType = new System.Windows.Forms.Label();
			this.SuspendLayout();
			// 
			// lblColumnName
			// 
			this.lblColumnName.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblColumnName.BackColor = System.Drawing.Color.Transparent;
			this.lblColumnName.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblColumnName.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblColumnName.Location = new System.Drawing.Point(2, 2);
			this.lblColumnName.Name = "lblColumnName";
			this.lblColumnName.Size = new System.Drawing.Size(108, 21);
			this.lblColumnName.TabIndex = 0;
			this.lblColumnName.Text = "ColumnName";
			this.lblColumnName.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// cboFileColumn
			// 
			this.cboFileColumn.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.cboFileColumn.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cboFileColumn.FormattingEnabled = true;
			this.cboFileColumn.Location = new System.Drawing.Point(2, 46);
			this.cboFileColumn.Name = "cboFileColumn";
			this.cboFileColumn.Size = new System.Drawing.Size(108, 21);
			this.cboFileColumn.TabIndex = 1;
			this.cboFileColumn.SelectedIndexChanged += new System.EventHandler(this.cboSqlColumn_SelectedIndexChanged);
			// 
			// txtStaticInput
			// 
			this.txtStaticInput.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.txtStaticInput.Location = new System.Drawing.Point(2, 69);
			this.txtStaticInput.Name = "txtStaticInput";
			this.txtStaticInput.Size = new System.Drawing.Size(108, 20);
			this.txtStaticInput.TabIndex = 2;
			this.txtStaticInput.TextChanged += new System.EventHandler(this.txtStaticInput_TextChanged);
			// 
			// lblDataType
			// 
			this.lblDataType.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
			this.lblDataType.BackColor = System.Drawing.Color.Transparent;
			this.lblDataType.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblDataType.ForeColor = System.Drawing.SystemColors.ControlText;
			this.lblDataType.Location = new System.Drawing.Point(2, 23);
			this.lblDataType.Name = "lblDataType";
			this.lblDataType.Size = new System.Drawing.Size(108, 21);
			this.lblDataType.TabIndex = 3;
			this.lblDataType.Text = "DataType";
			this.lblDataType.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// ColumnMap
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.Transparent;
			this.Controls.Add(this.lblDataType);
			this.Controls.Add(this.txtStaticInput);
			this.Controls.Add(this.cboFileColumn);
			this.Controls.Add(this.lblColumnName);
			this.Name = "ColumnMap";
			this.Size = new System.Drawing.Size(112, 92);
			this.ResumeLayout(false);
			this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.Label lblColumnName;
		private System.Windows.Forms.ComboBox cboFileColumn;
		private System.Windows.Forms.TextBox txtStaticInput;
		private System.Windows.Forms.Label lblDataType;
	}
}
