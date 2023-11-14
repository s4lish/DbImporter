using DbImporter.Models;
using Microsoft.VisualBasic.FileIO;
using System.Data;

namespace DbImporter.Helpers
{
    public class CSVManager
    {
        public static InputInfo GetCsvInfo(string path)
        {
            var info = new InputInfo() { Status = false };

            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");

                // Read the header row
                string[] headerFields = parser.ReadFields();
                if (headerFields == null)
                    return info;

                info.ColumnCount = headerFields.Length;
                info.Status = true;

                // Set column information
                for (int col = 0; col < headerFields.Length; col++)
                {
                    info.ColInfos.Add(new ColInfo
                    {
                        Number = col + 1,
                        HeaderName = headerFields[col],
                        FirstValue = "",  // You can modify this if you want to read the first value
                        type = typeof(string)  // You can modify this based on the actual type
                    });
                }

                // Read the remaining rows to get the row count
                List<string[]> rows = new List<string[]>();
                while (!parser.EndOfData)
                {
                    string[] fields = parser.ReadFields();
                    if (fields != null)
                        rows.Add(fields);
                }

                info.RowCount = rows.Count;

                if (info.RowCount > 0)
                {
                    for (int col = 0; col < info.ColInfos.Count; col++)
                    {
                        info.ColInfos[col].FirstValue = rows[0][col];
                        //info.ColInfos[col].type = GetTypeFromString(rows[0][col]);
                    }
                }

                return info;
            }
        }

        public static DataTable? GetCsvList(string path)
        {
            DataTable dataTable = new DataTable("CSVData");


            using (TextFieldParser parser = new TextFieldParser(path))
            {
                parser.SetDelimiters(",");

                // Assume the first line contains column headers
                string[] fields = parser.ReadFields();
                foreach (string field in fields)
                {
                    // Add columns to DataTable using the values in the first line
                    DataColumn column = new DataColumn(field);
                    dataTable.Columns.Add(column);
                }

                while (!parser.EndOfData)
                {
                    // Read data from each line and add it to the DataTable
                    fields = parser.ReadFields();
                    if (fields != null)
                    {
                        DataRow dataRow = dataTable.NewRow();
                        for (int i = 0; i < fields.Length; i++)
                        {
                            dataRow[i] = fields[i];
                        }
                        dataTable.Rows.Add(dataRow);
                    }
                }
            }


            return dataTable;
        }

        //private static Type GetTypeFromString(string value)
        //{
        //    if (int.TryParse(value, out _))
        //    {
        //        return typeof(int);
        //    }
        //    if (long.TryParse(value, out _))
        //    {
        //        return typeof(long);
        //    }
        //    else if (float.TryParse(value, out _))
        //    {
        //        return typeof(float);
        //    }
        //    else if (double.TryParse(value, out _))
        //    {
        //        return typeof(double);
        //    }
        //    else if (decimal.TryParse(value, out _))
        //    {
        //        return typeof(decimal);
        //    }
        //    else if (DateTime.TryParse(value, out _))
        //    {
        //        return typeof(DateTime);
        //    }
        //    else
        //    {
        //        return typeof(string);
        //    }
        //}
    }
}
