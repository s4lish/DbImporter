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

            // Assuming you have a DataGridView named dataGridView1
        }

        private string InputfilePath = string.Empty;
        private string ConnectionString = string.Empty;
        private List<string> tables = new();
        private List<SqlColumnInfo> columnsOftable = new();
        private string? TableName = string.Empty;
        private InputInfo inputInfo = new();
        private FileTypeEnum fileTypeEnum;
        private void btnSelectExcel_Click(object sender, EventArgs e)
        {

            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "c:\\";
                openFileDialog.Filter = "Excel files (*.xlsx)|*.xlsx|CSV files (*.csv)|*.csv|Json files (*.json)|*.json";
                openFileDialog.FilterIndex = 2;
                openFileDialog.RestoreDirectory = true;

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    //Get the path of specified file
                    InputfilePath = openFileDialog.FileName;

                    //Read the contents of the file into a stream
                    //var fileStream = openFileDialog.OpenFile();

                    //using (StreamReader reader = new StreamReader(fileStream))
                    //{
                    //    fileContent = reader.ReadToEnd();
                    //}
                }
            }
            if (string.IsNullOrEmpty(InputfilePath)) return;

            lblLocation.Text = InputfilePath;

            FileInfo fileInfo = new FileInfo(InputfilePath);


            if (fileInfo.Extension == ".xlsx")
            {
                inputInfo = ExcelManager.GetExcelInfo(InputfilePath);

                if (!inputInfo.Status)
                {
                    MessageBox.Show("Excel has problem", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                fileTypeEnum = FileTypeEnum.Excel;
            }
            else if (fileInfo.Extension == ".csv")
            {
                inputInfo = CSVManager.GetCsvInfo(InputfilePath);
                if (!inputInfo.Status)
                {
                    MessageBox.Show("csv file has problem", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
                fileTypeEnum = FileTypeEnum.CSV;

            }
            else
            {
                lblLocation.Text = string.Empty;
                InputfilePath = string.Empty;
                MessageBox.Show("Not Support yet");
                return;
            }


            lblRows.Text = inputInfo.RowCount.ToString();
            lblColumns.Text = inputInfo.ColumnCount.ToString();
            //lblLocation.Text = filePath;

            gridShowColumns.DataSource = inputInfo.ColInfos;
        }

        private void SQLConnect_Click(object sender, EventArgs e)
        {
            ConnectionString = $"Server={txtServer.Text};Database={txtDatabase.Text};User Id={txtUsername.Text};Password={txtPassword.Text};";


            tables = SqlManager.GetTablesOfDatabase(ConnectionString);
            comboTables.Items.Clear();
            comboTables.Items.Add("Select Table");

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

        private async void btnImport_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(InputfilePath) || string.IsNullOrEmpty(ConnectionString))
            {
                MessageBox.Show("Excel path or Connection string is empty", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            bool status = false;
            loading.Visible = true;
            loading.Style = ProgressBarStyle.Marquee;

            await Task.Run(async () =>
            {
                DataTable? dataTable = null;
                if (fileTypeEnum == FileTypeEnum.Excel)
                {
                    dataTable = ExcelManager.GetExcelList(InputfilePath);
                }
                else if (fileTypeEnum == FileTypeEnum.CSV)
                {
                    dataTable = CSVManager.GetCsvList(InputfilePath);
                }
                else
                {
                    loading.Visible = false;
                    MessageBox.Show("File Type Not Supported", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

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

                if (inputInfo == null || !inputInfo.ColInfos.Any())
                {
                    loading.Visible = false;
                    MessageBox.Show("Table mapping info is empty", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (rdtypetableNew.Checked)
                {
                    var check = await SqlManager.CreateTable(ConnectionString, TableName, inputInfo.ColInfos, CheckPrimaryKey.Checked);

                    if (!check)
                    {
                        loading.Visible = false;
                        MessageBox.Show("Create New Table Problem", "Problem", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }

                status = await SqlManager.InsertBulk(ConnectionString, TableName, inputInfo.ColInfos, dataTable);
            });

            if (status)
            {
                MessageBox.Show("Excel Imported to Sql Server Successfully", "Imported", MessageBoxButtons.OK, MessageBoxIcon.Information);
                InputfilePath = string.Empty;
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

            txtTableName.Text = string.Empty;
            TableName = string.Empty;
            txtTableName.Enabled = false;
            comboTables.Enabled = true;
            CheckPrimaryKey.Enabled = false;
            AddComboBoxColumn();
        }



        private void rdtypetableNew_CheckedChanged(object sender, EventArgs e)
        {
            if (!rdtypetableNew.Checked) return;

            if (comboTables.HasChildren)
                comboTables.SelectedIndex = 0;

            TableName = string.Empty;
            txtTableName.Enabled = true;
            CheckPrimaryKey.Enabled = true;
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
                ValueMember = "Name",
                DisplayMember = "DisplayName",
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
