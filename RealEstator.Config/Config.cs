using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealEstator.Config
{
    public class Config
    {
        public List<ConfigFile> LoadConfig()
        {
            using (StreamReader r = new StreamReader("config.json"))
            {
                string json = r.ReadToEnd();
                List<ConfigFile> items = JsonConvert.DeserializeObject<List<ConfigFile>>(json);
                return items;
            }
        }

        public class ConfigFile
        {
            public string apiKey;
        }
    }
}
