using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;

namespace Bartata.NET
{
    [DataContract]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class JWTHeader
    {
        [DataMember(Name = "alg")]
        public string alg { get; set; }
        [DataMember(Name = "typ")]
        public string typ { get; set; }
        [DataMember(Name = "kid")]
        public string kid { get; set; }

        internal static JWTHeader FromJson(string json)
        {
            return JsonHelper.Deserialize<JWTHeader>(json);
        }
    }
}
