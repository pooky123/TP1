using System;
using System.Security.Cryptography;
using System.Text;
using System.Windows.Media;

namespace TP1
{
    public class Util
    {
        public static Color GetRandomColor(string raw)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(raw));
                string hex = BitConverter.ToString(data).Replace("-", string.Empty).Substring(0, 6);
                Color color = (Color)ColorConverter.ConvertFromString("#" + hex);
                return color;
            }
        }
    }
}
