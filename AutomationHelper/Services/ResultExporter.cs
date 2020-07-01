using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;
using AutomationHelper.Models;

namespace AutomationHelper.Services
{
    public class ResultExporter
    {
        public void ExportAsCsvFile<T>(string path, string exportFileName, List<IResultTable> objList)
        {
            var sb = new StringBuilder();
            var propertiesName = typeof(T).GetProperties();

            // title
            var resultTitleString = new List<string>();
            foreach (PropertyInfo item in propertiesName)
            {
                resultTitleString.Add(item.Name);
            }
            sb.AppendLine(string.Join(',', resultTitleString));

            // body
            foreach (var item in objList)
            {
                var resultString = new List<string>();
                foreach (var prop in propertiesName)
                {
                    var result = item.GetType().GetProperty(prop.Name).GetValue(item).ToString();
                    resultString.Add(result);
                }
                sb.AppendLine(string.Join(',',resultString));
            }

            // export as file
            path += @$"\{exportFileName}.csv";
            File.WriteAllText(path, sb.ToString(), Encoding.UTF8);
        }

        public void ExportAsJsonFile<T>(string path, string exportFileName, List<IResultTable> objList)
        {
            var jsonString = Newtonsoft.Json.JsonConvert.SerializeObject(objList);
            path += @$"\{exportFileName}.json";
            File.WriteAllText(path, jsonString, Encoding.UTF8);
        }
    }
}


