using Framework.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Framework.Config
{
    public class FW
    {
        public static Setting Settings { get; set; }
        public static string BaseDir { get {
                var location = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return location.Split("bin", StringSplitOptions.None).First();
            } } 
        public static void SetConfig()
        {
            Settings = JsonDataHelper.ToObject<Setting>("Config\\settings.json");
        }
    }
}
