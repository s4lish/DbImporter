using DbImporter.Models;
using System.Data;
using System.Data.SqlClient;

namespace DbImporter.Helpers
{
    public class SqlManager
    {
        public static List<string> GetTablesOfDatabase(string connectionString)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {

                // Open the connection
                connection.Open();

                // Get the list of tables using a SQL query
                DataTable tablesSchema = connection.GetSchema("Tables");

                List<string> tables = new List<string>();
                // Print the table names
                foreach (DataRow row in tablesSchema.Rows)
                {
                    string tableName = (string)row["TABLE_NAME"];
                    tables.Add(tableName);
                }

                return tables;

            }

        }

        public static async Task<List<string>> GetTableColumns(string tableName, string connectionString)
        {

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                await connection.OpenAsync();

                // Query to get column names
                string query = $"SELECT COLUMN_NAME,DATA_TYPE FROM INFORMATION_SCHEMA.COLUMNS WHERE TABLE_NAME = '{tableName}'";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    using (SqlDataReader reader = await command.ExecuteReaderAsync())
                    {
                        List<string> columnNames = new List<string>();

                        columnNames.Add("");
                        while (reader.Read())
                        {
                            string columnName = reader["COLUMN_NAME"].ToString();
                            string dataType = reader["DATA_TYPE"].ToString();
                            columnNames.Add($"{columnName} ({dataType})");
                        }
                        return columnNames;
                    }
                }
            }

        }

        public static async Task<bool> CheckTableExist(string connectionString, string tableName)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Check if the table exists
                    using (SqlCommand command = new SqlCommand($"IF OBJECT_ID('{tableName}', 'U') IS NOT NULL SELECT 1 ELSE SELECT 0", connection))
                    {
                        object? result = await command.ExecuteScalarAsync();

                        if (result != null && result != DBNull.Value)
                        {
                            return Convert.ToInt32(result) == 1;
                        }
                    }
                }

                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Problem");
                return false;
            }

        }

        public static async Task<bool> CreateTable(string connectionString, string tableName, List<ColInfo> colInfos)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    // Check if the table already exists
                    if (await CheckTableExist(connectionString, tableName))
                    {
                        MessageBox.Show($"Table '{tableName}' already exists.", "Info");
                        return false;
                    }
                    //$"CREATE TABLE {tableName} (ID INT PRIMARY KEY, Column1 VARCHAR(50), Column2 INT)"
                    string query = $"CREATE TABLE {tableName} (";

                    foreach (ColInfo colInfo in colInfos)
                    {
                        if (string.IsNullOrEmpty(colInfo.DatabaseColumnName)) continue;
                        query += $"{colInfo.DatabaseColumnName} {colInfo.DatabaseColumnType} NULL, ";
                    }

                    query = query.Substring(0, query.Length - 1);

                    query += ")";

                    // Create the table
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        await command.ExecuteNonQueryAsync();
                        MessageBox.Show($"Table '{tableName}' created successfully.", "Info");
                    }

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Problem");
                return false;
            }
        }

        public static async Task<bool> InsertBulk(string connectionString, string tableName, List<ColInfo> Colinfos, DataTable dataTable)
        {
            try
            {


                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    await connection.OpenAsync();

                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(connection))
                    {
                        bulkCopy.DestinationTableName = tableName;

                        // Map the columns from Excel to the columns in the database
                        // Example: bulkCopy.ColumnMappings.Add("ExcelColumn1", "SQLColumn1");

                        foreach (var colInfo in Colinfos)
                        {
                            if (string.IsNullOrEmpty(colInfo.DatabaseColumnName)) continue;

                            bulkCopy.ColumnMappings.Add(colInfo.HeaderName, colInfo.DatabaseColumnName);
                        }

                        // Provide the DataTable or DataReader containing your Excel data
                        // Example: bulkCopy.WriteToServer(yourDataTable);

                        // Commit the changes
                        await bulkCopy.WriteToServerAsync(dataTable);
                    }
                }


                return true;
            }
            catch (FormatException ex)
            {
                MessageBox.Show(ex.Message, "Excel Column Format is Not the Same With Database Side");
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Problem");
                return false;
            }
        }

    }
}
