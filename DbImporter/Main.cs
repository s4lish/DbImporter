using DbImporter.Helpers;
using DbImporter.Models;
using System.Data;

namespace DbImporter
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }
        private string ExcelfilePath = string.Empty;
        private string ConnectionString = string.Empty;
        private List<string> tables = new List<string>();
        private List<string> columnsOftable = new List<string>();
        private string? TableName = string.Empty;
        private void btnSelectExcel_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    ExcelfilePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }
            lblLocation.Text = ExcelfilePath;

            ExcelInfo info = ExcelManager.GetExcelInfo(ExcelfilePath);

            if (!info.Status)
            {
                MessageBox.Show("Excel has problem", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            lblRows.Text = info.RowCount.ToString();
            lblColumns.Text = info.ColumnCount.ToString();
            //lblLocation.Text = filePath;

            gridShowColumns.DataSource = info.ColInfos;
        }

        private void SQLConnect_Click(object sender, EventArgs e)
        {
            ConnectionString = $"Server={txtServer.Text};Database={txtDatabase.Text};User Id={txtUsername.Text};Password={txtPassword.Text};";


            tables = SqlManager.GetTablesOfDatabase(ConnectionString);
            comboTables.Items.Clear();
            comboTables.Items.Add("انتخاب جدول");

            foreach (var table in tables)
            {
                comboTables.Items.Add(table);
            }

            comboTables.SelectedIndex = 0;
            lblStatus.Text = "Connected Successfully";
            lblStatus.ForeColor = Color.Green;

        }

        private async void comboTables_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboTables.SelectedIndex == 0)
                return;

            TableName = comboTables.SelectedItem.ToString();
            columnsOftable = await SqlManager.GetTableColumns(TableName ?? "", ConnectionString);
            AddComboBoxColumn();
        }

        private async void button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(ExcelfilePath) || string.IsNullOrEmpty(ConnectionString))
            {
                MessageBox.Show("Excel path or Connection string is empty", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool status = false;
            loading.Visible = true;
            loading.Style = ProgressBarStyle.Marquee;

            await Task.Run(async () =>
            {
                var info = gridShowColumns.DataSource as List<ColInfo>;

                DataTable? dataTable = ExcelManager.GetExcelList(ExcelfilePath);

                if (dataTable == null)
                {
                    loading.Visible = false;
                    MessageBox.Show("Excel is empty", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (rdtypetableNew.Checked)
                {
                    if (string.IsNullOrEmpty(txtTableName.Text))
                    {
                        loading.Visible = false;
                        MessageBox.Show("Table name is empty", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    TableName = txtTableName.Text;
                }

                if (string.IsNullOrEmpty(TableName))
                {
                    loading.Visible = false;
                    MessageBox.Show("Table name is empty", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (info == null)
                {
                    loading.Visible = false;
                    MessageBox.Show("Table mapping info is empty", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (rdtypetableNew.Checked)
                {
                    var check = await SqlManager.CreateTable(ConnectionString, TableName, info);

                    if (!check)
                    {
                        loading.Visible = false;
                        MessageBox.Show("Create New Table Problem", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                status = await SqlManager.InsertBulk(ConnectionString, TableName, info, dataTable);
            });

            if (status)
            {
                MessageBox.Show("Excel Imported to Sql Server Successfully", "Imported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                ExcelfilePath = string.Empty;
                lblLocation.Text = string.Empty;
                txtTableName.Text = string.Empty;
                TableName = string.Empty;
                comboTables.SelectedIndex = 0;
            }

            loading.Visible = false;

        }

        private void rdtypetableSelect_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdtypetableSelect.Checked) return;

            txtTableName.Enabled = false;
            comboTables.Enabled = true;
            AddComboBoxColumn();
        }



        private void rdtypetableNew_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdtypetableNew.Checked) return;

            txtTableName.Enabled = true;
            comboTables.Enabled = false;
            AddTextBoxColumn();
        }

        private void AddComboBoxColumn()
        {
            // Create a DataGridViewComboBoxColumn
            DataGridViewComboBoxColumn comboBoxColumn = new DataGridViewComboBoxColumn
            {
                HeaderText = "DatabaseColumnName",
                Name = "DatabaseColumnName",
                DataPropertyName = "DatabaseColumnName", // Bind to the property in ColInfo class
                DataSource = columnsOftable, // Provide data source for ComboBox items
                Width = 250
            };


            // Remove the column if it already exists

            foreach (DataGridViewRow row in gridShowColumns.Rows)
            {
                row.Cells[4].Value = null;
            }
            gridShowColumns.Columns.RemoveAt(4);

            // Insert the ComboBox column at the specified index
            gridShowColumns.Columns.Insert(4, comboBoxColumn);
        }

        private void AddTextBoxColumn()
        {
            // Create a DataGridViewTextBoxColumn
            DataGridViewTextBoxColumn textBoxColumn = new DataGridViewTextBoxColumn
            {
                HeaderText = "DatabaseColumnName",
                Name = "DatabaseColumnName",
                DataPropertyName = "DatabaseColumnName", // Bind to the property in ColInfo class
                Width = 250,
            };


            // Remove the column if it already exists

            foreach (DataGridViewRow row in gridShowColumns.Rows)
            {
                row.Cells[4].Value = null;
            }
            gridShowColumns.Columns.RemoveAt(4);

            // Insert the ComboBox column at the specified index
            gridShowColumns.Columns.Insert(4, textBoxColumn);
        }

    }
}
