using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Bartata.NET.Sample.MVC.Models;
using DotNetOpenAuth.AspNet.Clients;
using Bartata.NET.MVC;

namespace Bartata.NET.Sample.MVC
{
    public static class AuthConfig
    {
        public static void RegisterAuth()
        {
            // 若要讓此網站的使用者使用其他網站 (如 Microsoft、Facebook 和 Twitter) 的帳戶登入，
            // 您必須更新此網站。如需詳細資訊，請造訪 http://go.microsoft.com/fwlink/?LinkID=252166

            {
                string appId = "pcg-oidc-demo-client", appSecret = "", realmHost = "https://iamlab.pouchen.com/auth/realms/pcg";
                OAuthWebSecurity.RegisterPcgIAMClient("PCGIAM", "PCG", appId, appSecret, realmHost);

            }

            {
                string appId = "pcg-oidc-demo-server", appSecret = "c848a486-8923-46c7-b1c1-6312667558cd", realmHost = "https://iamlab.pouchen.com/auth/realms/pcg";
                OAuthWebSecurity.RegisterPcgIAMClient("PCGIAM2", "PCG2", appId, appSecret, realmHost);
            }

            {
                string appId = "pcg-oidc-demo-server", appSecret = "c848a486-8923-46c7-b1c1-6312667558cd", realmHost = "https://iamlab.pouchen.com/auth/realms/generic";
                OAuthWebSecurity.RegisterPcgIAMClient("PCGIAM3", "PCG3", appId, appSecret, realmHost);
            }

            //OAuthWebSecurity.RegisterMicrosoftClient(
            //    clientId: "",
            //    clientSecret: "");

            //OAuthWebSecurity.RegisterTwitterClient(
            //    consumerKey: "",
            //    consumerSecret: "");

            //OAuthWebSecurity.RegisterFacebookClient(
            //    appId: "",
            //    appSecret: "");

            //OAuthWebSecurity.RegisterGoogleClient();
        }

        public static DotNetOpenAuth.AspNet.IAuthenticationClient client { get; set; }
    }
}
