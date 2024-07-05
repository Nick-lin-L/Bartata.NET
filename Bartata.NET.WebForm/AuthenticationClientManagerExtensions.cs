using DotNetOpenAuth.AspNet.Clients;
using Microsoft.AspNet.Membership.OpenAuth;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bartata.NET.WebForm
{
    public  static class AuthenticationClientManagerExtensions
    {
        public static void AddPcgIAM(this AuthenticationClientManager clients,string clientName,string providerName, string appId, string appSecret,string realmHost) {

            IDictionary<string, string> extraData = new Dictionary<string, string>();
            extraData.Add("appId", appId);
            extraData.Add("appSecret", appSecret);
            extraData.Add("realmHost", realmHost);
            
            OAuth2Client authClient = new PcgOAuth2Client(appId, appSecret, providerName, realmHost);

            OpenAuth.AuthenticationClients.Add(clientName,
                () => authClient,
                extraData);
        }
    }
}
