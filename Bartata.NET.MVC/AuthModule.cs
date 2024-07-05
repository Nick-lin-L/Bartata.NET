using Microsoft.Web.WebPages.OAuth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Web;
using WebMatrix.WebData;

namespace Bartata.NET.MVC
{
    public class AuthModule : IHttpModule
    {

        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PreRequestHandlerExecute += context_PreRequestHandlerExecute;
        }

        void context_PreRequestHandlerExecute(object sender, EventArgs e)
        {
            var app = (HttpApplication)sender;

            //if (!Regex.IsMatch(app.Context.Request.Path, Configuration.Config.UrlFilterPattern))
            //    return;


            if (app.Context.Session == null)
                return;


            var ProviderName = (string)app.Context.Session[PcgOAuth2Client._Pcg_OAuth2_ProviderName];
            var TokenResponse = (TokenResponse)app.Context.Session[PcgOAuth2Client._Pcg_OAuth2_Token];
            var UserData = (IDictionary<string, object>)app.Context.Session[PcgOAuth2Client._Pcg_OAuth2_UserData];

            if (String.IsNullOrEmpty(ProviderName))
                return;

            var AuthenticationClientData = GetAuthenticationClientData(ProviderName);
            if (AuthenticationClientData == null)
                throw new ObjectNotFoundException("Authentication client not found " + ProviderName);

            var appId = (String)AuthenticationClientData.ExtraData["appId"];
            var appSecret = (String)AuthenticationClientData.ExtraData["appSecret"];
            var realmHost = (String)AuthenticationClientData.ExtraData["realmHost"];

            var client = new PcgClient(appId, appSecret, realmHost);

            UserInfomationResponse userinfo;
            if (tryGetUserInfomation(client, TokenResponse.access_token, out userinfo))
            {
            }
            else
            {
                app.Context.Session.Abandon();
                app.Context.Request.Cookies.Clear();
                WebSecurity.Logout();
                app.Context.Response.Redirect(Configuration.Config.LoginUrl);
                return;
            }

            var expTime = new DateTime(1970, 1, 1, 0, 0, 0).AddSeconds(Convert.ToInt32(UserData["exp"])).ToLocalTime();
            var now = DateTime.Now.AddMinutes(Configuration.Config.ExpireThreshold);

            if (now < expTime)
                return;

            TokenResponse tokenRes;
            if (tryRefreshToken(client, TokenResponse.refresh_token, out tokenRes))
            {
                var userData = client.GetUserData(tokenRes.access_token, false);

                app.Context.Session[PcgOAuth2Client._Pcg_OAuth2_Token] = tokenRes;
                app.Context.Session[PcgOAuth2Client._Pcg_OAuth2_UserData] = userData;

            }
            else
            {
                app.Context.Session.Abandon();
                app.Context.Request.Cookies.Clear();
                WebSecurity.Logout();
                app.Context.Response.Redirect(Configuration.Config.LoginUrl);

            }
        }

        private bool tryGetUserInfomation(PcgClient client, string access_token, out UserInfomationResponse userinfo)
        {
            try
            {
                userinfo = client.GetUserInfomation(access_token);
            }
            catch
            {
                userinfo = null;
                return false;
            }
            return true;
        }

        private AuthenticationClientData GetAuthenticationClientData(string ProviderName)
        {
            foreach (var ClientData in Microsoft.Web.WebPages.OAuth.OAuthWebSecurity.RegisteredClientData)
            {
                var AllAuthenticationClient = ClientData.AuthenticationClient;
                if (AllAuthenticationClient.ProviderName != ProviderName)
                    continue;

                return ClientData;

            }

            return null;


        }

        private bool tryRefreshToken(PcgClient client, string refresh_token, out TokenResponse tokenRes)
        {
            try
            {
                tokenRes = client.RefreshToken(refresh_token);
            }
            catch
            {
                tokenRes = null;
                return false;
            }
            return true;
        }
    }
}
