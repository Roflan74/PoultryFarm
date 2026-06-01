using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PoultryFarm
{
    public class TableLocalization
    {
        public List<string> SourceColumns { get; set; }

        public List<string> LocalizeColumns { get; set; }

        public List<string> HideColumns { get; set; }

        public static TableLocalization Load(string tableName)
        {
            // Убедитесь, что папка в вашем проекте называется именно так
            string filename = Path.Combine(
                "localizetables",
                $"Table{tableName}.json");

            if (!File.Exists(filename))
                return null;

            string jsonString = File.ReadAllText(filename);

            var localize = JsonConvert
                .DeserializeObject<TableLocalization>(jsonString);

            return localize;
        }

        public string GetLocale(string columnName)
        {
            string result = columnName;

            int index = SourceColumns.IndexOf(columnName);
            if (index > -1)
            {
                result = LocalizeColumns[index];
            }

            return result;
        }
    }
}