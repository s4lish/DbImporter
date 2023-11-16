namespace DbImporter.Models
{
    public class SqlColumnInfo
    {
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public bool Is_Nullable { get; set; } = false;
        public string PKey { get; set; } = string.Empty;

        public string DisplayName
        {
            get
            {
                if (string.IsNullOrEmpty(Name)) return string.Empty;
                var nullable = Is_Nullable ? "(Nullable)" : "";
                return $"{Name} ({Type}){nullable}";

            }
        }
    }
}
