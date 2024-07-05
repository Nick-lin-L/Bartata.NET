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
    public class UserInfomationResponse
    {
        [DataMember(Name = "pccuid")]
        public string pccuid { get; set; }

        [DataMember(Name = "sub")]
        public string sub { get; set; }

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

        internal static UserInfomationResponse FromJson(string json)
        {
            return JsonHelper.Deserialize<UserInfomationResponse>(json);
        }
    }
}
