using Framework.Config;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Framework.Utils
{
    public static class JsonDataHelper
    {
        public static Ttype ToObject<Ttype>(string filePath)
        {
            var path = FW.BaseDir + filePath;
            if (!File.Exists(path))
            {
                throw new ArgumentNullException("The data file doesn't exist");
            }

            var data = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<Ttype>(data);
        }


        public static JObject ToJObject(string filePath)
        {
            var path = FW.BaseDir + filePath;
            if (!File.Exists(path))
            {
                throw new ArgumentNullException("The data file doesn't exist");
            }

            var data = File.ReadAllText(path);
            return JObject.Parse(data);
        }
    }
}
