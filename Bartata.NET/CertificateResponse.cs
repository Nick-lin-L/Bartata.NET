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
    public class CertificateResponse
    {

        [DataMember(Name = "keys")]
        public Key[] keys { get; set; }

        internal static CertificateResponse FromJson(string json)
        {
            return JsonHelper.Deserialize<CertificateResponse>(json);
        }
    }

    [DataContract]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public class Key
    {
        [DataMember(Name = "kid")]
        public string kid { get; set; }

        [DataMember(Name = "kty")]
        public string kty { get; set; }

        [DataMember(Name = "alg")]
        public string alg { get; set; }

        [DataMember(Name = "use")]
        public string use { get; set; }

        [DataMember(Name = "n")]
        public string n { get; set; }

        [DataMember(Name = "e")]
        public string e { get; set; }

        [DataMember(Name = "x5c")]
        public List<string> x5c { get; set; }

        [DataMember(Name = "x5t")]
        public string x5t { get; set; }

        [DataMember(Name = "x5t#S256")]
        public string X5tS256 { get; set; }
    }
}
