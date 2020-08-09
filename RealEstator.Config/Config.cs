using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstator.Config
{
    public static class GetConfig
    {
        public static List<Key> LoadConfig()
        {
            using (StreamReader sr = new StreamReader(@"C:\workspace\csharp\real-estator\config.json"))
            {
                string json = sr.ReadToEnd();
                List<Key> keys = JsonConvert.DeserializeObject<List<Key>>(json);
                return keys;
            }
        }
    }

    public class Key
    {
        public string zillow;
        public string google;
    }
}