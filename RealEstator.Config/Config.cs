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
            using (StreamReader sr = new StreamReader(@"C:\workspace\csharp\real-estator\config.txt"))
            {
                string line = sr.ReadToEnd();
                return line;
            }
        }
    }
}
