using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LifenomeAPI
{
    public class JsonHelper
    {
        public static String GetJson(Object obj, Type obj_type)
        {
            using (MemoryStream stream1 = new MemoryStream())
            {
                DataContractJsonSerializerSettings sett = new DataContractJsonSerializerSettings();
                DataContractJsonSerializer ser = new DataContractJsonSerializer(obj_type);
                ser.WriteObject(stream1, obj);
                stream1.Position = 0;
                StreamReader sr = new StreamReader(stream1);
                return sr.ReadToEnd();
            }
        }

        private static DataContractJsonSerializerSettings dcjss = new DataContractJsonSerializerSettings()
        {             
            UseSimpleDictionaryFormat = true
        };

        public static T Parse<T>(String json)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            using (MemoryStream stream1 = new MemoryStream())
            {
                UnicodeEncoding uniEncoding = new UnicodeEncoding();
                byte[] buff = uniEncoding.GetBytes(json);
                stream1.Write(buff, 0, buff.Length);
                stream1.Position = 0;
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T), dcjss);
                return (T)ser.ReadObject(stream1);
            }
        }

        public static Object Parse(String json, Type obj_type)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            using (MemoryStream stream1 = new MemoryStream())
            {
                UnicodeEncoding uniEncoding = new UnicodeEncoding();
                byte[] buff = uniEncoding.GetBytes(json);
                stream1.Write(buff, 0, buff.Length);
                stream1.Position = 0;
                DataContractJsonSerializer ser = new DataContractJsonSerializer(obj_type, dcjss);
                return ser.ReadObject(stream1);
            }
        }
    }
}
