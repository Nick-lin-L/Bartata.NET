using Microsoft.AspNet.Membership.OpenAuth;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Core;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Security;

namespace Bartata.NET.WebForm
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

            var AuthenticationClient = GetAuthenticationClient(ProviderName);
            if (AuthenticationClient == null)
                throw new ObjectNotFoundException("Authentication client not found " + ProviderName);

            var appId = AuthenticationClient.ExtraData["appId"];
            var appSecret = AuthenticationClient.ExtraData["appSecret"];
            var realmHost = AuthenticationClient.ExtraData["realmHost"];

            var client = new PcgClient(appId, appSecret, realmHost);

            UserInfomationResponse userinfo;
            if (tryGetUserInfomation(client, TokenResponse.access_token, out userinfo))
            {
            }
            else
            {
                app.Context.Session.Abandon();
                app.Context.Request.Cookies.Clear();
                FormsAuthentication.SignOut();
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
                FormsAuthentication.SignOut();
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

        private ProviderDetails GetAuthenticationClient(string ProviderName)
        {
            var AuthenticationClients = OpenAuth.AuthenticationClients;
            var AllAuthenticationClients = AuthenticationClients.GetAll();

            foreach (var AllAuthenticationClient in AllAuthenticationClients)
            {
                if (AllAuthenticationClient.ProviderName != ProviderName)
                    continue;

                return AllAuthenticationClient;

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
