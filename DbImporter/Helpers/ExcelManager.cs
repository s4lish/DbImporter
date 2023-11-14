using DbImporter.Models;
using OfficeOpenXml;
using System.Data;

namespace DbImporter.Helpers
{
    public class ExcelManager
    {
        public static ExcelInfo GetExcelInfo(string path)
        {
            var info = new ExcelInfo() { Status = false };
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorkbook workbook = package.Workbook;
                ExcelWorksheet? current = workbook.Worksheets.FirstOrDefault();
                if (current == null)
                    return info;
                info.RowCount = current.Dimension.End.Row - 1;
                info.ColumnCount = current.Dimension.End.Column;
                info.Status = true;

                for (int col = 1; col <= current.Dimension.End.Column; col++)
                {
                    info.ColInfos.Add(new ColInfo
                    {
                        Number = col,
                        HeaderName = $"{current.Cells[1, col].Text}",
                        FirstValue = $"{current.Cells[2, col].Text}",
                        type = current.Cells[2, col].Value != null ? current.Cells[2, col].Value.GetType() : typeof(string)
                    });
                }
                return info;
            }
        }

        public static DataTable? GetExcelList(string path)
        {
            var info = new ExcelInfo() { Status = false };
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage(new FileInfo(path)))
            {
                ExcelWorkbook workbook = package.Workbook;
                ExcelWorksheet? current = workbook.Worksheets.FirstOrDefault();
                if (current == null)
                    return null;

                if (current.Dimension.Rows == 0)
                    return null;

                DataTable dataTable = WorksheetToDataTable(current);

                return dataTable;
            }
        }


        private static DataTable WorksheetToDataTable(ExcelWorksheet worksheet)
        {
            DataTable dataTable = new DataTable(worksheet.Name);

            // Assume the first row contains column headers
            int startRow = worksheet.Dimension.Start.Row;
            int startCol = worksheet.Dimension.Start.Column;

            for (int col = startCol; col <= worksheet.Dimension.End.Column; col++)
            {
                // Add columns to DataTable using the values in the first row
                DataColumn column = new DataColumn(worksheet.Cells[startRow, col].Text);
                dataTable.Columns.Add(column);
            }

            for (int row = startRow + 1; row <= worksheet.Dimension.End.Row; row++)
            {
                // Add rows to DataTable using the values in each row
                DataRow dataRow = dataTable.NewRow();

                for (int col = startCol; col <= worksheet.Dimension.End.Column; col++)
                {
                    dataRow[col - startCol] = worksheet.Cells[row, col].Text;
                }

                dataTable.Rows.Add(dataRow);
            }

            return dataTable;
        }

    }
}
