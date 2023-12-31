﻿using System.ComponentModel;

namespace DbImporter.Models
{
    public class InputInfo
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
        [Browsable(false)]
        public string? DatabaseColumnType
        {
            get
            {
                if (type == null)
                    return "";

                return type switch
                {
                    Type t when t == typeof(string) => "[nvarchar](max)",
                    Type t when t == typeof(int) => "[int]",
                    Type t when t == typeof(long) => "[bigint]",
                    Type t when t == typeof(Int64) => "[bigint]",
                    Type t when t == typeof(DateTime) => "[datetime]",
                    Type t when t == typeof(double) => "[float]",
                    Type t when t == typeof(float) => "[float]",
                    Type t when t == typeof(decimal) => "[float]",

                    _ => "[nvarchar](max)"
                };
            }
        }

    }
}
