using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace APIAutomation.Models.Response.Utility
{
    public class HandleContent
    {
        public static T GetContent<T>(RestResponse response)
        {
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);
        }
    }
    public static T ParseJson<T>(string file)
    {
        return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));

    }
    public static string GetFilePath(string name)
    {
        string path=Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
        path = string.Format(path + "TestData\\{0}", name);
        return path;
    }
}
