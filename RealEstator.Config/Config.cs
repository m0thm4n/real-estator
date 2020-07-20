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
        public static string LoadConfig()
        {
            using (StreamReader r = new StreamReader("config.json"))
            {
                string json = r.ReadToEnd();
                string apiKey = JsonConvert.DeserializeObject<string>(json);
                return apiKey;
            }
        }
    }
}
