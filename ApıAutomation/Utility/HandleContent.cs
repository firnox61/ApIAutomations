﻿using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SpecFlow.Internal.Json;

namespace ApiAutomation.Utility
{//yardımcı program 
    public class HandleContent
    {
        public static T GetContent<T>(RestResponse response)
        {//cevaptan içeirik almak için kullanıyoruz
            var content = response.Content;
            return JsonConvert.DeserializeObject<T>(content);//döndürebilmek için
        }
        //bir yöntem oluşturalım json için
        public static T ParseJson<T>(string file)
        {
            return JsonConvert.DeserializeObject<T>(File.ReadAllText(file));//veri aktarım nesnesi içinokuma türü ne olursa olsun

        }
        public static string GetFilePath(string name)//feature i payloada dönüştürdük ve dosya yolunu belirtmemiz gerekiyor
        {
            string path = Path.GetFullPath(Path.Combine(AppDomain.CurrentDomain.BaseDirectory));
            path = string.Format(path + @"TestData\\{0}", name);//dosya yolunu belirtiyoruz ve adı yanına ekliyoruz
            //path = string.Format(path + @"TestData\\CreateUser.json");
            return path;//4çentik oluştu
        }

    }

}
