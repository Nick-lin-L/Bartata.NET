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
    public class PcgClient
    {
        /// <summary>
        /// realm host
        /// </summary>
        private string realmHost { get; set; }


	    /// <summary>
	    /// The authorization endpoint.
	    /// </summary>
        private string AuthorizationEndpoint
        {
            get {
                return GetConfiguration().authorization_endpoint;
            }
        }

	    /// <summary>
	    /// The token endpoint.
	    /// </summary>
	    private string TokenEndpoint
        {
            get {
                return GetConfiguration().token_endpoint;
            }
        }

	    /// <summary>
	    /// The user info endpoint.
	    /// </summary>
	    private string UserInfoEndpoint
        {
            get {
                return GetConfiguration().userinfo_endpoint;
            }
        }

        /// <summary>
        /// The certificate endpoint.
        /// </summary>
        private string CertificateEndpoint
        {
            get
            {
                return GetConfiguration().jwks_uri;
            }
        }

        /// <summary>
        /// The configuration endpoint.
        /// </summary>
        private string ConfigurationEndpoint
        {
            get
            {
                return Path.Combine(realmHost, ".well-known/openid-configuration");
            }
        }

        /// <summary>
        /// The logout endpoint.
        /// </summary>
        private string LogoutEndpoint
        {
            get
            {
                return GetConfiguration().end_session_endpoint;
            }
        }

	    /// <summary>
	    /// The _app id.
	    /// </summary>
	    private readonly string appId;

	    /// <summary>
	    /// The _app secret.
	    /// </summary>
	    private readonly string appSecret;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:Bartata.NET.PccOAuth2Client" /> class.
        /// </summary>
        /// <param name="appId">
        /// The app id.
        /// </param>
        /// <param name="appSecret">
        /// The app secret.
        /// </param>
        /// <param name="realmHost">
        /// 
        /// </param>
        public PcgClient(string appId, string appSecret, string realmHost)
        {
            this.appId = appId;
            this.appSecret = appSecret;
            this.realmHost = realmHost;
        }

	    /// <summary>
	    /// The get service login url.
	    /// </summary>
	    /// <param name="returnUrl">
	    /// The return url.
	    /// </param>
	    /// <returns>An absolute URI.</returns>
        public Uri GetServiceLoginUrl(Uri returnUrl)
	    {
            UriBuilder uriBuilder = new UriBuilder(AuthorizationEndpoint);
            uriBuilder.AppendQueryArgument("client_id", appId);
            uriBuilder.AppendQueryArgument("scope", "openid");
            uriBuilder.AppendQueryArgument("response_type", "code");
            uriBuilder.AppendQueryArgument("redirect_uri", returnUrl.AbsoluteUri);

		    return uriBuilder.Uri;
	    }

        /// <summary>
        /// The get service logout url.
        /// </summary>
        /// <param name="returnUrl">
        /// The return url.
        /// </param>
        /// <param name="id_token">
        /// The id_token from token response.
        /// </param>
        /// <returns>An absolute URI.</returns>
        public Uri GetServiceLogoutUrl(Uri returnUrl, string id_token = null)
        {
            UriBuilder uriBuilder = new UriBuilder(LogoutEndpoint);
            uriBuilder.AppendQueryArgument("post_logout_redirect_uri", returnUrl.AbsoluteUri);
            if (!string.IsNullOrEmpty(id_token))
            {
                uriBuilder.AppendQueryArgument("id_token_hint", id_token);
            }
            return uriBuilder.Uri;
        }



        /// <summary>
        /// The get user data from decrypted access token.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <param name="isVerify">
        /// Verify Signature
        /// </param>
        /// <returns>A dictionary of profile data</returns>
        public IDictionary<string, object> GetUserData(string accessToken, bool isVerify = true) {
            return this.GetUserData<IDictionary<string, object>>(accessToken, isVerify);
        }

        /// <summary>
        /// The get user data from decrypted access token.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="accessToken">
        /// The access token.
        /// </param>
        /// <param name="isVerify">
        /// Verify Signature
        /// </param>
        /// <returns>profile data</returns>
        public T GetUserData<T>(string accessToken,bool isVerify = true)
        {
            T claims;
            if (isVerify)
            {
                CertificateResponse certRes = GetCertificate();
                var urlEncoder = new JwtBase64UrlEncoder();
                var rsaKey = RSA.Create();
                rsaKey.ImportParameters(new RSAParameters()
                {
                    Modulus = urlEncoder.Decode(certRes.keys[0].n),
                    Exponent = urlEncoder.Decode(certRes.keys[0].e)
                });

                claims = new JwtBuilder()
                    .WithAlgorithm(new RS256Algorithm(rsaKey))
                    .MustVerifySignature()
                    .Decode<T>(accessToken);
            }
            else
            {

                claims = new JwtBuilder().Decode<T>(accessToken);
            }

            return claims;
        }

        /// <summary>
        /// get certs from endpoint protocol/openid-connect/certs
        /// </summary>
        /// <returns></returns>
        public CertificateResponse GetCertificate()
        {
            // solve TLS https://stackoverflow.com/questions/33761919/tls-1-2-in-net-framework-4-0
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var request = (HttpWebRequest)WebRequest.Create(CertificateEndpoint);
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var certRes = CertificateResponse.FromJson(responseString);
            return certRes;
        
        }

        /// <summary>
        /// get full token from endpoint protocol/openid-connect/token
        /// </summary>
        /// <param name="returnUrl"></param>
        /// <param name="authorizationCode"></param>
        /// <param name="isUrlEncode"></param>
        /// <returns></returns>
        public TokenResponse QeuryToken(Uri returnUrl, string authorizationCode,bool isUrlEncode = true)
        {

            // solve TLS https://stackoverflow.com/questions/33761919/tls-1-2-in-net-framework-4-0
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var request = (HttpWebRequest)WebRequest.Create(TokenEndpoint);
            
            var postBody = "grant_type=authorization_code";
            postBody += "&client_id=" + this.appId;
            if (!string.IsNullOrEmpty(this.appSecret))
            { 
                postBody += "&client_secret=" + this.appSecret; 
            }
            postBody += "&code=" + authorizationCode;
            postBody += "&redirect_uri=" + (isUrlEncode ? HttpUtility.UrlEncode(returnUrl.AbsoluteUri) : returnUrl.AbsoluteUri);
            var data = Encoding.ASCII.GetBytes(postBody);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var tokenResponse = TokenResponse.FromJson(responseString);
            return tokenResponse;
        }

        /// <summary>
        /// get user information from endpoint protocol/openid-connect/userinfo
        /// </summary>
        /// <param name="access_token"></param>
        /// <returns></returns>
        public UserInfomationResponse GetUserInfomation(string access_token)
        {

            // solve TLS https://stackoverflow.com/questions/33761919/tls-1-2-in-net-framework-4-0
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var request = (HttpWebRequest)WebRequest.Create(UserInfoEndpoint);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + access_token);

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var userInfo = UserInfomationResponse.FromJson(responseString);
            return userInfo;
        
        }

        public TokenResponse RefreshToken(string refreshToken)
        {

            // solve TLS https://stackoverflow.com/questions/33761919/tls-1-2-in-net-framework-4-0
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var request = (HttpWebRequest)WebRequest.Create(TokenEndpoint);
            
            var postBody = "grant_type=refresh_token";
            postBody += "&client_id=" + this.appId;
            if (!string.IsNullOrEmpty(this.appSecret))
            {
                postBody += "&client_secret=" + this.appSecret;
            }
            postBody += "&refresh_token=" + refreshToken;
            var data = Encoding.ASCII.GetBytes(postBody);

            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;

            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var tokenResponse = TokenResponse.FromJson(responseString);
            return tokenResponse;
        }
        /// <summary>
        /// get certs from endpoint protocol/openid-connect/certs
        /// </summary>
        /// <returns></returns>
        public ConfigurationResponse GetConfiguration()
        {
            // solve TLS https://stackoverflow.com/questions/33761919/tls-1-2-in-net-framework-4-0
            System.Net.ServicePointManager.SecurityProtocol = (SecurityProtocolType)3072;
            var request = (HttpWebRequest)WebRequest.Create(ConfigurationEndpoint);
            request.Method = "GET";

            var response = (HttpWebResponse)request.GetResponse();
            var responseString = new StreamReader(response.GetResponseStream()).ReadToEnd();
            var conf = ConfigurationResponse.FromJson(responseString);
            return conf;

        }

    }
}
