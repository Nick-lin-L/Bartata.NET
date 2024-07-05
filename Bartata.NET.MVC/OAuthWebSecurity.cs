using DotNetOpenAuth.AspNet.Clients;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

//namespace Microsoft.Web.WebPages.OAuth
namespace Bartata.NET.MVC
{
    public static class OAuthWebSecurity
    {
        public static void RegisterPcgIAMClient(string clientName, string providerName, string appId, string appSecret, string realmHost)
        {
            IDictionary<string, Object> extraData = new Dictionary<string, Object>();
            extraData.Add("appId", appId);
            extraData.Add("appSecret", appSecret);
            extraData.Add("realmHost", realmHost);

            OAuth2Client authClient = new PcgOAuth2Client(appId, appSecret,
                providerName, realmHost);
            Microsoft.Web.WebPages.OAuth.OAuthWebSecurity.RegisterClient(authClient, clientName, extraData);
        }
    }
}
