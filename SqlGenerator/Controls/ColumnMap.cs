using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using SqlGenerator.Entities;
using SqlGenerator.Extenders;


namespace SqlGenerator.Controls
{
	public partial class ColumnMap : UserControl
	{
		private const int NoMapping = -2;
		private const int StaticData = -1;

		public event EventHandler IsModified;

		private string StaticInput => txtStaticInput.Text.Trim();
	    public int FileColumnIndex => (int)cboFileColumn.SelectedValue;

	    public SqlColumn SqlColumn { get; private set; }
		public bool IsMapped => (int)cboFileColumn.SelectedValue != NoMapping;
	    public bool IsValid => (int)cboFileColumn.SelectedValue != StaticData || StaticInput.IsSpecified();
        public string ValueMarker => StaticInput.IsSpecified() ? SqlColumn.CreateValueString(StaticInput) : $"{{{FileColumnIndex}}}";



	    public ColumnMap(SqlColumn sqlColumn, ICollection<FileColumn> fileColumns)
		{
			InitializeComponent();

			SqlColumn = sqlColumn;

			// FileColumn
			cboFileColumn.DisplayMember = "Name";
			cboFileColumn.ValueMember = "Index";
			if (!fileColumns.Any(c => c.Index < 0))
			{
				fileColumns.Add(new FileColumn { Index = NoMapping, Name = "----(No Mapping)------" });
				fileColumns.Add(new FileColumn { Index = StaticData, Name = "----(Static Data)------" });
			}
			cboFileColumn.DataSource = fileColumns.OrderBy(c => c.Index).ToList();

			lblColumnName.Text = sqlColumn.ColumnName;
			lblDataType.Text = sqlColumn.DisplayDataType + (sqlColumn.IsIdentity ? " Identity" : string.Empty) + (sqlColumn.AllowDBNull ? "  NULL" : string.Empty);

			var matchingFileColumn = fileColumns.SingleOrDefault(f => f.Name == sqlColumn.ColumnName);
			if (matchingFileColumn != null)
				cboFileColumn.SelectedValue = matchingFileColumn.Index;

			SetShowState();
		}


		private void SetShowState()
		{
			lblColumnName.BackColor = SqlColumn.IsKey ? System.Drawing.Color.Lavender : System.Drawing.Color.Transparent;

			if (cboFileColumn.SelectedValue != null && (int)cboFileColumn.SelectedValue == NoMapping)
			{
				lblColumnName.ForeColor = System.Drawing.Color.DarkGray;
			}
			else if (cboFileColumn.SelectedValue != null && (int)cboFileColumn.SelectedValue == StaticData)
			{
				txtStaticInput.Text = null;
				lblColumnName.ForeColor = System.Drawing.Color.DodgerBlue;
			}
			else
			{
				lblColumnName.ForeColor = System.Drawing.Color.Black;
			}
			lblDataType.ForeColor = lblColumnName.ForeColor;

			txtStaticInput.Visible = cboFileColumn.SelectedValue != null && (int)cboFileColumn.SelectedValue == StaticData;
			if (txtStaticInput.Enabled)
				txtStaticInput.Focus();
		}

		private void cboSqlColumn_SelectedIndexChanged(object sender, EventArgs e)
		{
			SetShowState();
			if (IsModified != null)
			{
				IsModified(this, EventArgs.Empty);
			}
		}

		private void txtStaticInput_TextChanged(object sender, EventArgs e)
		{
			if (IsModified != null)
			{
				IsModified(this, EventArgs.Empty);
			}
		}
	}
}