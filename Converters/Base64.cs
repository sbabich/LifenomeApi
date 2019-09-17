using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifenomeAPI.Converters
{
    public static class Base64
    {
        public static string Encode(byte[] buff, int length)
        {
            return Convert.ToBase64String(buff, 0, length);
        }

        public static string Encode(byte[] buff)
        {
            return Encode(buff, buff.Length);
        }

        public static byte[] Decode(String base64)
        {
            return Convert.FromBase64String(base64);
        }
    }

}
