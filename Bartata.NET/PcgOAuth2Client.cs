using DotNetOpenAuth.AspNet;
using DotNetOpenAuth.AspNet.Clients;
using DotNetOpenAuth.Messaging;
using JWT;
using JWT.Algorithms;
using JWT.Builder;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization.Json;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;

namespace Bartata.NET
{
    public class PcgOAuth2Client : OAuth2Client
    {

        public const string _Pcg_OAuth2_Token = "_Pcg_OAuth2_Token";
        public const string _Pcg_OAuth2_UserData = "_Pcg_OAuth2_UserData";
        public const string _Pcg_OAuth2_ProviderName = "_Pcg_OAuth2_ProviderName";

        private readonly PcgClient client;

	    /// <summary>
        /// Initializes a new instance of the <see cref="T:Bartata.NET.PcgOAuth2Client" /> class.
        /// with providerName PCG  & realmHost "https://iamlab.pouchen.com/auth/realms/pcg"
	    /// </summary>
	    /// <param name="appId">
	    /// The app id.
	    /// </param>
	    /// <param name="appSecret">
	    /// The app secret.
	    /// </param>
        public PcgOAuth2Client(string appId, string appSecret)
            : this(appId, appSecret, "PCG", "https://iamlab.pouchen.com/auth/realms/pcg")
	    {
	    }

        public PcgOAuth2Client(string appId, string appSecret, string providerName, string realmHost)
            : this(new PcgClient(appId, appSecret, realmHost), providerName)
        {
        }

        public PcgOAuth2Client(PcgClient client, string providerName)
            : base(providerName)
        {
            this.client = client;
        }

	    /// <summary>
	    /// The get service login url.
	    /// </summary>
	    /// <param name="returnUrl">
	    /// The return url.
	    /// </param>
	    /// <returns>An absolute URI.</returns>
	    protected override Uri GetServiceLoginUrl(Uri returnUrl)
	    {
            return client.GetServiceLoginUrl(returnUrl);
	    }

        /// <summary>
	    /// The get user data.
	    /// </summary>
	    /// <param name="accessToken">
	    /// The access token.
	    /// </param>
	    /// <returns>A dictionary of profile data.</returns>
	    protected override IDictionary<string, string> GetUserData(string accessToken)
        {
            var claims =  client.GetUserData<IDictionary<string, object>>(accessToken);

            Dictionary<string, string> dictionary = new Dictionary<string, string>();
            foreach (var key in claims.Keys) {
                if (claims[key] is string) {
                    dictionary.Add(key, (string)claims[key]);
                }
            }
            dictionary.Add("id", (string)claims["pccuid"]);
            dictionary.Add("username", (string)claims["name"]);

            return dictionary;
	    }

	    /// <summary>
	    /// Obtains an access token given an authorization code and callback URL.
	    /// </summary>
	    /// <param name="returnUrl">
	    /// The return url.
	    /// </param>
	    /// <param name="authorizationCode">
	    /// The authorization code.
	    /// </param>
	    /// <returns>
	    /// The access token.
	    /// </returns>
	    protected override string QueryAccessToken(Uri returnUrl, string authorizationCode)
	    {

            var tokenResponse = client.QeuryToken(returnUrl, authorizationCode);

            return tokenResponse.access_token;

	    }


        public override AuthenticationResult VerifyAuthentication(HttpContextBase context, Uri returnPageUrl)
        {
            string text = context.Request.QueryString["code"];
            if (string.IsNullOrEmpty(text))
            {
                return AuthenticationResult.Failed;
            }

            TokenResponse tokenResponse;
            try
            {
                tokenResponse = client.QeuryToken(returnPageUrl, text);
                if (tokenResponse == null)
                {
                    return AuthenticationResult.Failed;
                }
            }
            catch (Exception ex)
            {
                return new AuthenticationResult(ex);

            }

            IDictionary<string, object> claims;

            try
            {
                claims = client.GetUserData<IDictionary<string, object>>(tokenResponse.access_token);
                if (claims == null)
                {
                    return AuthenticationResult.Failed;
                }
            }
            catch (Exception ex) {
                return new AuthenticationResult(ex);
            }

            Dictionary<string, string> userData = new Dictionary<string, string>();
            foreach (var key in claims.Keys)
            {
                if (claims[key] is string)
                {
                    userData.Add(key, (string)claims[key]);
                }
            }
            userData.Add("id", (string)claims["pccuid"]);
            userData.Add("username", (string)claims["name"]);

            var providerUserId = userData["pccuid"];
            string name;
            if (!userData.TryGetValue("username", out name) && !userData.TryGetValue("name", out name))
            {
                name = providerUserId;
            }

            context.Session[_Pcg_OAuth2_Token] = tokenResponse;
            context.Session[_Pcg_OAuth2_UserData] = claims;
            context.Session[_Pcg_OAuth2_ProviderName] = ProviderName;

            return new AuthenticationResult(true, ProviderName, providerUserId, name, userData);
        }


    }
}
