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
    public class TokenResponse
    {
        [DataMember(Name = "access_token")]
        public string access_token { get; set; }

        [DataMember(Name = "expires_in")]
        public int expires_in { get; set; }

        [DataMember(Name = "refresh_expires_in")]
        public int refresh_expires_in { get; set; }

        [DataMember(Name = "refresh_token")]
        public string refresh_token { get; set; }

        [DataMember(Name = "token_type")]
        public string token_type { get; set; }

        [DataMember(Name = "id_token")]
        public string id_token { get; set; }

        [DataMember(Name = "not-before-policy")]
        public int NotBeforePolicy { get; set; }

        [DataMember(Name = "session_state")]
        public string session_state { get; set; }

        [DataMember(Name = "scope")]
        public string scope { get; set; }

        public static TokenResponse FromJson(string json)
        {
            return JsonHelper.Deserialize<TokenResponse>(json);
        }
    }
}
