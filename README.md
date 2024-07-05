# Pcg OAuth2.0 implementation for .NET

This library supports PCG IAM OAuth2.0 login 

- [Pcg OAuth2.0 implementation for .NET](#pcg-oauth20-implementation-for-net)
  - [Supported .NET versions](#supported-net-versions)
  - [Dependences](#dependences)
  - [Bartata.NET](#bartatanet)
    - [PcgOAuth2Client constructors](#pcgoauth2client-constructors)
    - [asp.net webform add register auth client](#aspnet-webform-add-register-auth-client)
    - [asp.net MVC add register auth client](#aspnet-mvc-add-register-auth-client)
    - [how to use registered client in real use case](#how-to-use-registered-client-in-real-use-case)
    - [logout](#logout)
    - [refresh token](#refresh-token)
    - [filter flow](#filter-flow)
    - [get token & user data](#get-token--user-data)
    - [app sample](#app-sample)
    - [app embedded webview sample](#app-embedded-webview-sample)
  - [Contributing to this project](#contributing-to-this-project)
  - [Issues](#issues)


## Supported .NET versions

- .NET Framework 4.0

## Dependences

- JWT (8.4.2)
- Newtonsoft.Json (10.0.3)
- DotNetOpenAuth.AspNet (4.1.4.12333)
- DotNetOpenAuth.Core (4.1.4.12333)
- DotNetOpenAuth.OAuth.Consumer (4.1.4.12333)
- DotNetOpenAuth.OAuth.Core (4.1.4.12333)
- DotNetOpenAuth.OpenId.Core (4.1.4.12333)
- DotNetOpenAuth.OpenId.RelyingParty (4.1.4.12333)

## Bartata.NET

### PcgOAuth2Client constructors

```csharp
    public class PcgOAuth2Client : OAuth2Client
    {

	    /// <summary>
	    /// Initializes a new instance of the PcgOAuth2Client class.
	    /// </summary>
	    /// <param name="appId">
	    /// The app id.
	    /// </param>
	    /// <param name="appSecret">
	    /// The app secret.
	    /// </param>
	    /// </summary>
	    /// <param name="providerName">
	    /// The provider name.
	    /// </param>
	    /// </summary>
	    /// <param name="realmHost">
	    /// The realm host.
	    /// </param>
        public PcgOAuth2Client(string appId, string appSecret, string providerName, string realmHost);

	    /// <summary>
	    /// Initializes a new instance of the PcgOAuth2Client class.
	    /// with providerName PCG  & realmHost "https://iamlab.pouchen.com/auth/realms/pcg"
	    /// </summary>
	    /// <param name="appId">
	    /// The app id.
	    /// </param>
	    /// <param name="appSecret">
	    /// The app secret.
	    /// </param>
        public PcgOAuth2Client(string appId, string appSecret);

        public PcgOAuth2Client(PcgClient client, string providerName);
    }

```

### asp.net webform add register auth client

```csharp

            {
                string appId = "pcg-oidc-demo-client", appSecret = "", realmHost = "https://iamlab.pouchen.com/auth/realms/pcg";
                IDictionary<string, string> extraData = new Dictionary<string, string>();
                extraData.Add("appId", appId);
                extraData.Add("appSecret", appSecret);
                extraData.Add("realmHost", realmHost);

                OAuth2Client authClient = new PcgOAuth2Client(appId, appSecret);

                OpenAuth.AuthenticationClients.Add("PCGIAM",
                    () => authClient,
                    extraData);

            }

            {
                string appId = "pcg-oidc-demo-server", appSecret = "c848a486-8923-46c7-b1c1-6312667558cd", realmHost = "https://iamlab.pouchen.com/auth/realms/pcg";
                IDictionary<string, string> extraData = new Dictionary<string, string>();
                extraData.Add("appId", appId);
                extraData.Add("appSecret", appSecret);
                extraData.Add("realmHost", realmHost);

                OAuth2Client authClient = new PcgOAuth2Client(appId, appSecret,"PCG2", realmHost);

                OpenAuth.AuthenticationClients.Add("PCGIAM2",
                    () => authClient,
                    extraData);
            }

            {
                string appId = "pcg-oidc-demo-server", appSecret = "c848a486-8923-46c7-b1c1-6312667558cd", realmHost = "https://iamlab.pouchen.com/auth/realms/generic";
                IDictionary<string, string> extraData = new Dictionary<string, string>();
                extraData.Add("appId", appId);
                extraData.Add("appSecret", appSecret);
                extraData.Add("realmHost", realmHost);
                
                OAuth2Client authClient = new PcgOAuth2Client(appId, appSecret, "PCG3", realmHost);

                OpenAuth.AuthenticationClients.Add("PCGIAM3",
                    () => authClient,
                    extraData);
            }

            {
                string appId = "pcg-oidc-demo-server", appSecret = "c848a486-8923-46c7-b1c1-6312667558cd", realmHost = "https://iamlab.pouchen.com/auth/realms/pcg";
                OpenAuth.AuthenticationClients.AddPcgIAM("PCGIAM2", "PCG2", appId, appSecret, realmHost);
            }

```

### asp.net MVC add register auth client

```csharp

            {
                string appId = "pcg-oidc-demo-client", appSecret = "", realmHost = "https://iamlab.pouchen.com/auth/realms/pcg";
                IDictionary<string, Object> extraData = new Dictionary<string, Object>();
                extraData.Add("appId", appId);
                extraData.Add("appSecret", appSecret);
                extraData.Add("realmHost", realmHost);

                OAuth2Client authClient = new PcgOAuth2Client(appId, appSecret);

                OAuthWebSecurity.RegisterClient(authClient,"PCGIAM",extraData);

            }

            {
                string appId = "pcg-oidc-demo-server", appSecret = "c848a486-8923-46c7-b1c1-6312667558cd", realmHost = "https://iamlab.pouchen.com/auth/realms/pcg";
                IDictionary<string, Object> extraData = new Dictionary<string, Object>();
                extraData.Add("appId", appId);
                extraData.Add("appSecret", appSecret);
                extraData.Add("realmHost", realmHost);

                OAuth2Client authClient = new PcgOAuth2Client(appId, appSecret,
                    "PCG2", realmHost);

                OAuthWebSecurity.RegisterClient(authClient, "PCGIAM2", extraData);
            }

            {
                string appId = "pcg-oidc-demo-server", appSecret = "c848a486-8923-46c7-b1c1-6312667558cd", realmHost = "https://iamlab.pouchen.com/auth/realms/generic";
                IDictionary<string, Object> extraData = new Dictionary<string, Object>();
                extraData.Add("appId", appId);
                extraData.Add("appSecret", appSecret);
                extraData.Add("realmHost", realmHost);

                OAuth2Client authClient = new PcgOAuth2Client(appId, appSecret,
                    "PCG3", realmHost);

                OAuthWebSecurity.RegisterClient(authClient,"PCGIAM3",extraData);
            }

            {
                string appId = "pcg-oidc-demo-server", appSecret = "c848a486-8923-46c7-b1c1-6312667558cd", realmHost = "https://iamlab.pouchen.com/auth/realms/pcg";
                OAuthWebSecurity.RegisterPcgIAMClient("PCGIAM2", "PCG2", appId, appSecret, realmHost);
            }

```

### how to use registered client in real use case

```csharp


            var isLogin = false;
            //get current IAM redirect back provicer name
            var ProviderName = OpenAuth.GetProviderNameFromCurrentRequest();
            var isLogging = !String.IsNullOrEmpty(ProviderName);
            var redirectUrl = "~/Default.aspx";

            if (isLogin)
            {
                //redirect to home
                Response.Redirect("FinSignCenterWeb/PccApHome2.aspx?ApID=" + Convert.ToString(ConfigurationManager.AppSettings["ApID"]));
            }
            else
            {
                if (isLogging)
                {
                    // verify current IAM redirect back auth code & jwt decrypt & jwk signature
                    var authResult = OpenAuth.VerifyAuthentication(redirectUrl);
                    if (!authResult.IsSuccessful)
                    {
                        throw new Exception("login fail.");
                    }

                }
                else
                {
                    //Redirect to IAM
                    OpenAuth.RequestAuthentication("PCG", redirectUrl);
                }

            }

```

### logout 

```csharp

            var client new PcgClient("pcg-oidc-demo-client", "", "https://iamlab.pouchen.com/auth/realms/pcg");
            var redirectUrl = client.GetServiceLogoutUrl(uriBuilder.Uri);
            Response.Redirect(redirectUrl.AbsoluteUri);

```

### refresh token

```csharp

            var client new PcgClient("pcg-oidc-demo-client", "", "https://iamlab.pouchen.com/auth/realms/pcg");
            var tokenRes = client.RefreshToken((string)Session["refreshtoken"]);

```

### filter flow & SSO

```xml
...
  <configSections>
    <section name="pcgClientConfig" type="Bartata.NET.Configuration, Bartata.NET" />
  </configSections>
...
  <system.webServer>
    <modules runAllManagedModulesForAllRequests="true">
      <add name="AuthModule" type="Bartata.NET.WebForm.AuthModule,Bartata.NET.WebForm" />
    </modules>
  </system.webServer>
...
  <pcgClientConfig loginUrl="~/Account/Login" expireThreshold="60"/>
...
```

```csharp

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
```

### get token & user data

```csharp

var userdata = (IDictionary<string, object>)Session[Bartata.NET.PcgOAuth2Client._Pcg_OAuth2_UserData];

var token = (Bartata.NET.TokenResponse)Session[Bartata.NET.PcgOAuth2Client._Pcg_OAuth2_Token];

```

### app sample

```csharp
            // https://github.com/googlesamples/oauth-apps-for-windows/blob/3f14353db7085ea1927ae6bc9951f39bf6946ec7/OAuthConsoleApp/OAuthConsoleApp/Program.cs#L59
            var client = new PcgClient("pcg-oidc-demo-client", "", "https://iamlab.pouchen.com/auth/realms/pcg");
            string redirectUri = "http://localhost:" + 8080 + "/";
            var http = new HttpListener();
            http.Prefixes.Add(redirectUri);
            http.Start();
            var authorizationRequest = client.GetServiceLoginUrl(new Uri(redirectUri)).AbsoluteUri;

            //start browser to login
            Process.Start(authorizationRequest);
            var context = http.GetContext();

            //response close page
            var response = context.Response;
            string responseString = "<html><head><script>close();</script></head><body>Please return to the app.</body></html>";
            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            response.ContentLength64 = buffer.Length;
            var responseOutput = response.OutputStream;
            responseOutput.Write(buffer, 0, buffer.Length);
            responseOutput.Close();
            http.Stop();

            //get user auth data
            NameValueCollection outer = HttpUtility.ParseQueryString(context.Request.Url.Query);
            var code = outer["code"];
            var token = client.QeuryToken(new Uri(redirectUri), code);
            var userData = client.GetUserData(token.access_token, false);


```

### app embedded webview sample

- require [webview2 runtime](https://developer.microsoft.com/zh-tw/microsoft-edge/webview2/)

## Contributing to this project

Anyone and everyone is welcome to contribute. 

- Bug reports
- Feature requests
- Pull requests

Project location : http://sd2ap2-dev01.pouchen.com:3300/leo.yen/Bartata.NET

## Others 

- [webview2 sample](https://github.com/MicrosoftEdge/WebView2Samples)