using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Framework.Utils
{
    public static class Extensions
    {
        public static Color ParseFromStr(string rgbColor)
        {
            string[] numbers = rgbColor.Replace(")", "").Substring(rgbColor.IndexOf('(') + 1).Split(',');
            int r = Convert.ToInt32(numbers[0].Trim());
            int g = Convert.ToInt32(numbers[1].Trim());
            int b = Convert.ToInt32(numbers[2].Trim());
            return Color.FromArgb(r, g, b);
        }
    }
}
