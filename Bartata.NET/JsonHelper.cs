using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Text;

namespace Bartata.NET
{
    public class JsonHelper
    {
        public static T Deserialize<T>(string json)
        {
            MemoryStream stream = new MemoryStream(Encoding.UTF8.GetBytes(json));
            DataContractJsonSerializer dataContractJsonSerializer = new DataContractJsonSerializer(typeof(T));
            return (T)dataContractJsonSerializer.ReadObject(stream);
        }
    }
}
