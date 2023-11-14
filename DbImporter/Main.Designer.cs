namespace DbImporter
{
    partial class Main
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            openFileDialogExcel = new OpenFileDialog();
            btnSelectExcel = new Button();
            lblLocation = new Label();
            panelExcelInfo = new Panel();
            lblColumns = new Label();
            lblRows = new Label();
            label1 = new Label();
            lbl1 = new Label();
            gridShowColumns = new DataGridView();
            Number = new DataGridViewTextBoxColumn();
            HeaderName = new DataGridViewTextBoxColumn();
            FirstValue = new DataGridViewTextBoxColumn();
            type = new DataGridViewTextBoxColumn();
            DatabaseColumnName = new DataGridViewTextBoxColumn();
            txtServer = new TextBox();
            label2 = new Label();
            txtUsername = new TextBox();
            txtPassword = new TextBox();
            txtDatabase = new TextBox();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            SQLConnect = new Button();
            comboTables = new ComboBox();
            lblStatus = new Label();
            btnImport = new Button();
            rdtypetableSelect = new RadioButton();
            rdtypetableNew = new RadioButton();
            txtTableName = new TextBox();
            loading = new ProgressBar();
            CheckPrimaryKey = new CheckBox();
            panelExcelInfo.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)gridShowColumns).BeginInit();
            SuspendLayout();
            // 
            // openFileDialogExcel
            // 
            openFileDialogExcel.FileName = "openFileDialog1";
            // 
            // btnSelectExcel
            // 
            btnSelectExcel.Location = new Point(12, 12);
            btnSelectExcel.Name = "btnSelectExcel";
            btnSelectExcel.Size = new Size(140, 23);
            btnSelectExcel.TabIndex = 0;
            btnSelectExcel.Text = "File Select";
            btnSelectExcel.UseVisualStyleBackColor = true;
            btnSelectExcel.Click += btnSelectExcel_Click;
            // 
            // lblLocation
            // 
            lblLocation.AutoSize = true;
            lblLocation.Location = new Point(158, 16);
            lblLocation.Name = "lblLocation";
            lblLocation.Size = new Size(16, 15);
            lblLocation.TabIndex = 1;
            lblLocation.Text = "...";
            // 
            // panelExcelInfo
            // 
            panelExcelInfo.Controls.Add(lblColumns);
            panelExcelInfo.Controls.Add(lblRows);
            panelExcelInfo.Controls.Add(label1);
            panelExcelInfo.Controls.Add(lbl1);
            panelExcelInfo.Location = new Point(12, 41);
            panelExcelInfo.Name = "panelExcelInfo";
            panelExcelInfo.Size = new Size(345, 55);
            panelExcelInfo.TabIndex = 2;
            // 
            // lblColumns
            // 
            lblColumns.AutoSize = true;
            lblColumns.Location = new Point(146, 30);
            lblColumns.Name = "lblColumns";
            lblColumns.Size = new Size(13, 15);
            lblColumns.TabIndex = 3;
            lblColumns.Text = "0";
            // 
            // lblRows
            // 
            lblRows.AutoSize = true;
            lblRows.Location = new Point(146, 10);
            lblRows.Name = "lblRows";
            lblRows.Size = new Size(13, 15);
            lblRows.TabIndex = 2;
            lblRows.Text = "0";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(21, 30);
            label1.Name = "label1";
            label1.Size = new Size(119, 15);
            label1.TabIndex = 1;
            label1.Text = "Number of Columns:";
            // 
            // lbl1
            // 
            lbl1.AutoSize = true;
            lbl1.Location = new Point(21, 10);
            lbl1.Name = "lbl1";
            lbl1.Size = new Size(99, 15);
            lbl1.TabIndex = 0;
            lbl1.Text = "Number of Rows:";
            // 
            // gridShowColumns
            // 
            gridShowColumns.AllowUserToAddRows = false;
            gridShowColumns.AllowUserToDeleteRows = false;
            gridShowColumns.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            gridShowColumns.BackgroundColor = SystemColors.ButtonFace;
            gridShowColumns.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            gridShowColumns.Columns.AddRange(new DataGridViewColumn[] { Number, HeaderName, FirstValue, type, DatabaseColumnName });
            gridShowColumns.Location = new Point(12, 215);
            gridShowColumns.Name = "gridShowColumns";
            gridShowColumns.RowTemplate.Height = 25;
            gridShowColumns.Size = new Size(994, 471);
            gridShowColumns.TabIndex = 3;
            // 
            // Number
            // 
            Number.DataPropertyName = "Number";
            Number.HeaderText = "Number";
            Number.Name = "Number";
            Number.ReadOnly = true;
            // 
            // HeaderName
            // 
            HeaderName.DataPropertyName = "HeaderName";
            HeaderName.HeaderText = "HeaderName";
            HeaderName.Name = "HeaderName";
            HeaderName.ReadOnly = true;
            HeaderName.Width = 250;
            // 
            // FirstValue
            // 
            FirstValue.DataPropertyName = "FirstValue";
            FirstValue.HeaderText = "FirstValue";
            FirstValue.Name = "FirstValue";
            FirstValue.ReadOnly = true;
            FirstValue.Width = 150;
            // 
            // type
            // 
            type.DataPropertyName = "type";
            type.HeaderText = "type";
            type.Name = "type";
            type.ReadOnly = true;
            // 
            // DatabaseColumnName
            // 
            DatabaseColumnName.DataPropertyName = "DatabaseColumnName";
            DatabaseColumnName.HeaderText = "DatabaseColumnName";
            DatabaseColumnName.Name = "DatabaseColumnName";
            DatabaseColumnName.Width = 250;
            // 
            // txtServer
            // 
            txtServer.Location = new Point(67, 134);
            txtServer.Name = "txtServer";
            txtServer.Size = new Size(100, 23);
            txtServer.TabIndex = 4;
            txtServer.Text = ".";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(10, 116);
            label2.Name = "label2";
            label2.Size = new Size(142, 15);
            label2.TabIndex = 5;
            label2.Text = "Sql Server Database Login";
            // 
            // txtUsername
            // 
            txtUsername.Location = new Point(244, 134);
            txtUsername.Name = "txtUsername";
            txtUsername.Size = new Size(100, 23);
            txtUsername.TabIndex = 6;
            txtUsername.Text = "sa";
            // 
            // txtPassword
            // 
            txtPassword.Location = new Point(411, 134);
            txtPassword.Name = "txtPassword";
            txtPassword.Size = new Size(100, 23);
            txtPassword.TabIndex = 7;
            txtPassword.Text = "ali@1368";
            // 
            // txtDatabase
            // 
            txtDatabase.Location = new Point(578, 134);
            txtDatabase.Name = "txtDatabase";
            txtDatabase.Size = new Size(100, 23);
            txtDatabase.TabIndex = 8;
            txtDatabase.Text = "VosulGas";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 137);
            label3.Name = "label3";
            label3.Size = new Size(55, 15);
            label3.TabIndex = 9;
            label3.Text = "Address :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(174, 138);
            label4.Name = "label4";
            label4.Size = new Size(69, 15);
            label4.TabIndex = 10;
            label4.Text = "User name :";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(348, 137);
            label5.Name = "label5";
            label5.Size = new Size(63, 15);
            label5.TabIndex = 11;
            label5.Text = "Password :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(517, 138);
            label6.Name = "label6";
            label6.Size = new Size(61, 15);
            label6.TabIndex = 12;
            label6.Text = "Database :";
            // 
            // SQLConnect
            // 
            SQLConnect.Location = new Point(705, 134);
            SQLConnect.Name = "SQLConnect";
            SQLConnect.Size = new Size(140, 23);
            SQLConnect.TabIndex = 13;
            SQLConnect.Text = "Connect";
            SQLConnect.UseVisualStyleBackColor = true;
            SQLConnect.Click += SQLConnect_Click;
            // 
            // comboTables
            // 
            comboTables.FormattingEnabled = true;
            comboTables.Location = new Point(102, 174);
            comboTables.Name = "comboTables";
            comboTables.Size = new Size(260, 23);
            comboTables.TabIndex = 14;
            comboTables.SelectedIndexChanged += comboTables_SelectedIndexChanged;
            // 
            // lblStatus
            // 
            lblStatus.AutoSize = true;
            lblStatus.Location = new Point(849, 138);
            lblStatus.Name = "lblStatus";
            lblStatus.Size = new Size(16, 15);
            lblStatus.TabIndex = 16;
            lblStatus.Text = "...";
            // 
            // btnImport
            // 
            btnImport.Location = new Point(772, 41);
            btnImport.Name = "btnImport";
            btnImport.Size = new Size(216, 23);
            btnImport.TabIndex = 17;
            btnImport.Text = "Import To Database";
            btnImport.UseVisualStyleBackColor = true;
            btnImport.Click += btnImport_Click;
            // 
            // rdtypetableSelect
            // 
            rdtypetableSelect.AutoSize = true;
            rdtypetableSelect.Checked = true;
            rdtypetableSelect.Location = new Point(10, 175);
            rdtypetableSelect.Name = "rdtypetableSelect";
            rdtypetableSelect.Size = new Size(92, 19);
            rdtypetableSelect.TabIndex = 18;
            rdtypetableSelect.TabStop = true;
            rdtypetableSelect.Tag = "tableType";
            rdtypetableSelect.Text = "Select Table :";
            rdtypetableSelect.UseVisualStyleBackColor = true;
            rdtypetableSelect.CheckedChanged += rdtypetableSelect_CheckedChanged;
            // 
            // rdtypetableNew
            // 
            rdtypetableNew.AutoSize = true;
            rdtypetableNew.Location = new Point(426, 175);
            rdtypetableNew.Name = "rdtypetableNew";
            rdtypetableNew.Size = new Size(85, 19);
            rdtypetableNew.TabIndex = 19;
            rdtypetableNew.Tag = "tableType";
            rdtypetableNew.Text = "New Table :";
            rdtypetableNew.UseVisualStyleBackColor = true;
            rdtypetableNew.CheckedChanged += rdtypetableNew_CheckedChanged;
            // 
            // txtTableName
            // 
            txtTableName.Enabled = false;
            txtTableName.Location = new Point(517, 175);
            txtTableName.Name = "txtTableName";
            txtTableName.PlaceholderText = "table name";
            txtTableName.Size = new Size(189, 23);
            txtTableName.TabIndex = 20;
            // 
            // loading
            // 
            loading.Location = new Point(772, 73);
            loading.Name = "loading";
            loading.Size = new Size(216, 13);
            loading.TabIndex = 21;
            loading.Visible = false;
            // 
            // CheckPrimaryKey
            // 
            CheckPrimaryKey.AutoSize = true;
            CheckPrimaryKey.Enabled = false;
            CheckPrimaryKey.Location = new Point(718, 175);
            CheckPrimaryKey.Name = "CheckPrimaryKey";
            CheckPrimaryKey.Size = new Size(127, 19);
            CheckPrimaryKey.TabIndex = 22;
            CheckPrimaryKey.Text = "Add Id Primary Key";
            CheckPrimaryKey.UseVisualStyleBackColor = true;
            // 
            // Main
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1018, 698);
            Controls.Add(CheckPrimaryKey);
            Controls.Add(loading);
            Controls.Add(txtTableName);
            Controls.Add(rdtypetableNew);
            Controls.Add(rdtypetableSelect);
            Controls.Add(btnImport);
            Controls.Add(lblStatus);
            Controls.Add(comboTables);
            Controls.Add(SQLConnect);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(txtDatabase);
            Controls.Add(txtPassword);
            Controls.Add(txtUsername);
            Controls.Add(label2);
            Controls.Add(txtServer);
            Controls.Add(gridShowColumns);
            Controls.Add(panelExcelInfo);
            Controls.Add(lblLocation);
            Controls.Add(btnSelectExcel);
            MinimumSize = new Size(1016, 726);
            Name = "Main";
            Text = "DB Importer";
            panelExcelInfo.ResumeLayout(false);
            panelExcelInfo.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)gridShowColumns).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private OpenFileDialog openFileDialogExcel;
        private Button btnSelectExcel;
        private Label lblLocation;
        private Panel panelExcelInfo;
        private Label lblRows;
        private Label label1;
        private Label lbl1;
        private Label lblColumns;
        private ListView listView1;
        private DataGridView gridShowColumns;
        private TextBox txtServer;
        private Label label2;
        private TextBox txtUsername;
        private TextBox txtPassword;
        private TextBox txtDatabase;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Button SQLConnect;
        private ComboBox comboTables;
        private Label lblStatus;
        private Button btnImport;
        private RadioButton rdtypetableSelect;
        private RadioButton rdtypetableNew;
        private TextBox txtTableName;
        private ProgressBar loading;
        private DataGridViewTextBoxColumn Number;
        private DataGridViewTextBoxColumn HeaderName;
        private DataGridViewTextBoxColumn FirstValue;
        private DataGridViewTextBoxColumn type;
        private DataGridViewTextBoxColumn DatabaseColumnName;
        private CheckBox CheckPrimaryKey;
    }
}