namespace DbImporter.Models
{
    public class ExcelInfo
    {
        public bool Status { get; set; }
        public int RowCount { get; set; }
        public int ColumnCount { get; set; }

        public List<ColInfo> ColInfos { get; set; } = new List<ColInfo>();
    }

    public class ColInfo
    {
        public int Number { get; set; }
        public string HeaderName { get; set; } = string.Empty;
        public string FirstValue { get; set; } = string.Empty;
        public Type? type { get; set; }
        public string? DatabaseColumnName { get; set; } = string.Empty;
    }
}
