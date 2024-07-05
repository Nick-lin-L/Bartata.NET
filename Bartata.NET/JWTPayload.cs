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
    public class JWTPayload
    {
        [DataMember(Name = "exp")]
        public int exp { get; set; }

        [DataMember(Name = "iat")]
        public int iat { get; set; }

        [DataMember(Name = "auth_time")]
        public int auth_time { get; set; }

        [DataMember(Name = "jti")]
        public string jti { get; set; }

        [DataMember(Name = "iss")]
        public string iss { get; set; }

        [DataMember(Name = "aud")]
        public string aud { get; set; }

        [DataMember(Name = "sub")]
        public string sub { get; set; }

        [DataMember(Name = "typ")]
        public string typ { get; set; }

        [DataMember(Name = "azp")]
        public string azp { get; set; }

        [DataMember(Name = "session_state")]
        public string session_state { get; set; }

        [DataMember(Name = "at_hash")]
        public string at_hash { get; set; }

        [DataMember(Name = "acr")]
        public string acr { get; set; }

        [DataMember(Name = "pccuid")]
        public string pccuid { get; set; }

        [DataMember(Name = "uid")]
        public string uid { get; set; }

        [DataMember(Name = "employee_type")]
        public string employee_type { get; set; }

        [DataMember(Name = "email_verified")]
        public bool email_verified { get; set; }

        [DataMember(Name = "name")]
        public string name { get; set; }

        [DataMember(Name = "preferred_username")]
        public string preferred_username { get; set; }

        [DataMember(Name = "given_name")]
        public string given_name { get; set; }

        [DataMember(Name = "family_name")]
        public string family_name { get; set; }

        [DataMember(Name = "email")]
        public string email { get; set; }

        internal static JWTPayload FromJson(string json)
        {
            return JsonHelper.Deserialize<JWTPayload>(json);
        }
    }
}
