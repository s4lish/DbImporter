using DbImporter.Models;
using System.Data;
using System.Text.Json;

namespace DbImporter.Helpers
{
    public class JsonManager
    {

        public static InputInfo GetJsonArrayInfo(string jsonPath)
        {
            var info = new InputInfo() { Status = false };

            // Assuming you have a class for JsonInfo and JsonColInfo similar to InputInfo and ColInfo

            // Read the JSON array from the specified path
            string jsonArrayText = File.ReadAllText(jsonPath);

            // Deserialize the JSON array into a list of dictionaries
            var jsonArray = JsonSerializer.Deserialize<List<Dictionary<string, object>>>(jsonArrayText);

            if (jsonArray == null || jsonArray.Count == 0)
                return info;

            // Extract information from the JSON array

            info.RowCount = jsonArray.Count;
            info.ColumnCount = jsonArray[0].Count;
            info.Status = true;

            // Assuming the keys in the first dictionary represent column names
            foreach (var key in jsonArray[0].Keys)
            {
                info.ColInfos.Add(new ColInfo
                {
                    Number = info.ColInfos.Count + 1,
                    HeaderName = key,
                    FirstValue = jsonArray[0][key]?.ToString() ?? "",
                    type = typeof(string) // You may need to adjust this based on your data
                });
            }
            return info;
        }


        public static DataTable? GetJsonlList(string jsonPath)
        {
            string jsonArrayText = File.ReadAllText(jsonPath);
            DataTable? dataTable = new();

            JsonElement data = JsonSerializer.Deserialize<JsonElement>(jsonArrayText);

            if (data.ValueKind != JsonValueKind.Array)
            {
                return dataTable;
            }

            var dataArray = data.EnumerateArray();
            JsonElement firstObject = dataArray.First();

            var firstObjectProperties = firstObject.EnumerateObject();
            foreach (var element in firstObjectProperties)
            {
                dataTable.Columns.Add(element.Name);
            }
            foreach (var obj in dataArray)
            {
                var objProperties = obj.EnumerateObject();
                DataRow newRow = dataTable.NewRow();
                foreach (var item in objProperties)
                {
                    newRow[item.Name] = item.Value;
                }
                dataTable.Rows.Add(newRow);
            }
            return dataTable;

        }




    }
}
