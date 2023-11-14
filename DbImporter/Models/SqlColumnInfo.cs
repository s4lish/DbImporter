namespace DbImporter.Models
{
    public class SqlColumnInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(Name)) return string.Empty;
                return $"{Name} ({Type})";

            }
        }
    }
}
