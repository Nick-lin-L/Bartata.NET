using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.ComponentModel;
using System.Text;
using Bartata.NET;
using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using JWT.Exceptions;
using System.Security.Cryptography;
using JWT.Builder;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Diagnostics;
using System.Collections.Specialized;
using System.Web;


namespace Bartata.NET.Test
{
    [TestClass]
    public class UnitTest1
    {
        /// <summary>
        /// Test token response deserialize 
        /// </summary>
        [TestMethod]
        public void TestMethod1()
        {
            var json = File.ReadAllText("..\\..\\TextFile1.txt");

            var data = TokenResponse.FromJson(json);

            Assert.AreEqual("eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJCRWhKU082c202RXM5MDc5Qnp3dTBuR3JNcUpXNXFmWENDa0RCUnVPU3JRIn0.eyJleHAiOjE2MzMzMzM2NTIsImlhdCI6MTYzMzMzMDA1MiwiYXV0aF90aW1lIjoxNjMzMzI3MDcwLCJqdGkiOiI5OWMwODlhZi01MWRkLTQ3MWItOWFkZS1mM2E3MGQwOThlOWYiLCJpc3MiOiJodHRwczovL2lhbWxhYi5wb3VjaGVuLmNvbS9hdXRoL3JlYWxtcy9wY2ciLCJhdWQiOiJhY2NvdW50Iiwic3ViIjoiNTBiZDk3MzYtZjQzMi00YWY1LWFkZjUtY2RiYTk4YTgwNDkxIiwidHlwIjoiQmVhcmVyIiwiYXpwIjoicGNnLW9pZGMtZGVtby1jbGllbnQiLCJzZXNzaW9uX3N0YXRlIjoiMjgwNWI3YzItMDBmOC00MDQ5LWFiMTAtNGNiMmIxNTY0ZTIyIiwiYWNyIjoiMCIsImFsbG93ZWQtb3JpZ2lucyI6WyJodHRwOi8vbG9jYWxob3N0OjgwODAiLCJodHRwOi8vMTI3LjAuMC4xOjgwODAiXSwicmVhbG1fYWNjZXNzIjp7InJvbGVzIjpbImRlZmF1bHQtcm9sZXMtcGNnIiwib2ZmbGluZV9hY2Nlc3MiLCJ1bWFfYXV0aG9yaXphdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwiLCJwY2N1aWQiOiIyMDE5MDIwMDAwMTM0NCIsInVpZCI6Imxlby55ZW4iLCJlbXBsb3llZV90eXBlIjoiMDEt6ZuG5ZyY5ZOh5belIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJuYW1lIjoiTGVvIFllbiDpoY_lv5fou5IiLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJsZW8ueWVuIiwiZ2l2ZW5fbmFtZSI6Iumhj-W_l-i7kiIsImZhbWlseV9uYW1lIjoi6aGP5b-X6LuSIiwiZW1haWwiOiJsZW8ueWVuQHBvdWNoZW4uY29tIn0.VzhKtBAFjs4-cgQRhC6w_5Je3H2gtb5saLGfiDgulX2wBVePdxrrqQT1l17Ae5zg9mi5XDNfbOP2tSN_SdDDLQJV8M0dIJaC9uZ3T44KdPnSzk9Dtv7tmbphzLwm0Cfz6TweT4LzbCA0GDVgJwQ8MxjyWyriZgecJeBGdtI7ynECzGgR29A0TFZLL3tChml8-WtS_a8PCVLg44ezouIqiuO_dMlJIUiWmrZxgctUtaj_4VdNbKcsOeiu9QbosflPoTA2WqqooKVVM0mEj7NauXXpd-YTRb9nSWkUkrThwOV7agAnYKVBcuKu3rK7drbFHh8gw1TG8dRAFlnsNlFz5Q", data.access_token);
            Assert.AreEqual(3600,data.expires_in);
            Assert.AreEqual(47418,data.refresh_expires_in);
            Assert.AreEqual("eyJhbGciOiJIUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICI0MTM5OTBjOC1iNDA4LTQyYzQtOThhOC0yYTI0OGM3NjZjYzQifQ.eyJleHAiOjE2MzMzNzc0NzAsImlhdCI6MTYzMzMzMDA1MiwianRpIjoiMTVmNTkyZmYtMzUyZC00OWMzLWJhZTYtNmM0NTQyMzFiODcxIiwiaXNzIjoiaHR0cHM6Ly9pYW1sYWIucG91Y2hlbi5jb20vYXV0aC9yZWFsbXMvcGNnIiwiYXVkIjoiaHR0cHM6Ly9pYW1sYWIucG91Y2hlbi5jb20vYXV0aC9yZWFsbXMvcGNnIiwic3ViIjoiNTBiZDk3MzYtZjQzMi00YWY1LWFkZjUtY2RiYTk4YTgwNDkxIiwidHlwIjoiUmVmcmVzaCIsImF6cCI6InBjZy1vaWRjLWRlbW8tY2xpZW50Iiwic2Vzc2lvbl9zdGF0ZSI6IjI4MDViN2MyLTAwZjgtNDA0OS1hYjEwLTRjYjJiMTU2NGUyMiIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwifQ.AOMv3XFtVKZR-uBbIYgWPJbzuaECKKTwDxU6MEEhZ0U",data.refresh_token);
            Assert.AreEqual("Bearer",data.token_type);
            Assert.AreEqual("eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJCRWhKU082c202RXM5MDc5Qnp3dTBuR3JNcUpXNXFmWENDa0RCUnVPU3JRIn0.eyJleHAiOjE2MzMzMzM2NTIsImlhdCI6MTYzMzMzMDA1MiwiYXV0aF90aW1lIjoxNjMzMzI3MDcwLCJqdGkiOiJlNTJjMjkyYS02YTczLTRkN2ItOTM0Ny00Y2E0MDkxNDdmYjUiLCJpc3MiOiJodHRwczovL2lhbWxhYi5wb3VjaGVuLmNvbS9hdXRoL3JlYWxtcy9wY2ciLCJhdWQiOiJwY2ctb2lkYy1kZW1vLWNsaWVudCIsInN1YiI6IjUwYmQ5NzM2LWY0MzItNGFmNS1hZGY1LWNkYmE5OGE4MDQ5MSIsInR5cCI6IklEIiwiYXpwIjoicGNnLW9pZGMtZGVtby1jbGllbnQiLCJzZXNzaW9uX3N0YXRlIjoiMjgwNWI3YzItMDBmOC00MDQ5LWFiMTAtNGNiMmIxNTY0ZTIyIiwiYXRfaGFzaCI6IllsdDFZQ29UTjVSRjlhbEt5aGFTalEiLCJhY3IiOiIwIiwicGNjdWlkIjoiMjAxOTAyMDAwMDEzNDQiLCJ1aWQiOiJsZW8ueWVuIiwiZW1wbG95ZWVfdHlwZSI6IjAxLembhuWcmOWToeW3pSIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwibmFtZSI6IkxlbyBZZW4g6aGP5b-X6LuSIiwicHJlZmVycmVkX3VzZXJuYW1lIjoibGVvLnllbiIsImdpdmVuX25hbWUiOiLpoY_lv5fou5IiLCJmYW1pbHlfbmFtZSI6Iumhj-W_l-i7kiIsImVtYWlsIjoibGVvLnllbkBwb3VjaGVuLmNvbSJ9.PROjL-xmF6_T1fhyjAw2pF_UuojOyWud-vFRymkbPaKTtTbf7FSV64BnMHFmZDglr1yykhWuDVtiQqQ3Ktkeb0Atj0_alB6NZicUVrvTy1rkjNmwzpmFTuFpXfgO32RVAYSnrOejfWxg3337ckRw9XDVZLoNrsqxKMlt2-be35AJdPiyA19BfSXAL-FnTI1aaZEOhV6GELSWjYrderEDYlcc5E4tkosKFwz0skQ318Zk0KITMwE1Q8AElrTVei_f01S7O2h1Cmg3Q1Jv3S6aEawQW95jiWg2AZsCpNEHPxhf8CT7EJPur1weVKGA8_axrzoj10KPbwRm5ToaysVVLw",data.id_token);
            Assert.AreEqual(0,data.NotBeforePolicy);
            Assert.AreEqual("2805b7c2-00f8-4049-ab10-4cb2b1564e22",data.session_state);
            Assert.AreEqual("openid profile email",data.scope);

        }

        /// <summary>
        /// Test JWT decode without verify signature.
        /// </summary>
        [TestMethod]
        public void TestMethod2()
        {
            var token = "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJYeVZUbzZ5VjlQN0xMcWJmMjZlbXFSMUFiTG5LeHBKYmw2MnlfanItQkM4In0.eyJleHAiOjE2MzQ1NDYxMjUsImlhdCI6MTYzNDUxNzMyNSwiYXV0aF90aW1lIjoxNjM0NTE3MzAzLCJqdGkiOiI0YjU3M2IwMC1mMWZhLTQzZDctOTEyZi03MTI5Yjc0MGI2ZGYiLCJpc3MiOiJodHRwczovL2lhbS5wb3VjaGVuLmNvbS9hdXRoL3JlYWxtcy9wY2ciLCJhdWQiOiJwY2ctc2NtLWNsaWVudCIsInN1YiI6ImQ3NjE1NWI2LWMxNTUtNDk0ZC1iY2IzLTg4ZTE3NmZiYzIyYyIsInR5cCI6IklEIiwiYXpwIjoicGNnLXNjbS1jbGllbnQiLCJzZXNzaW9uX3N0YXRlIjoiZmE0NjFhOGQtODdjYy00NWRmLTg1NDgtYzdmMDBkZjYwMWEyIiwiYXRfaGFzaCI6InJaWjhmVGNpNm9rclh1em94UldGd2ciLCJhY3IiOiIwIiwicGNjdWlkIjoiMjAxOTAyMDAwMDEzNDQiLCJ1aWQiOiJsZW8ueWVuIiwiZW1wbG95ZWVfdHlwZSI6IjAxLembhuWcmOWToeW3pSIsImVtYWlsX3ZlcmlmaWVkIjpmYWxzZSwibmFtZSI6IkxlbyBZZW4g6aGP5b-X6LuSIiwicHJlZmVycmVkX3VzZXJuYW1lIjoibGVvLnllbiIsImdpdmVuX25hbWUiOiLpoY_lv5fou5IiLCJmYW1pbHlfbmFtZSI6Iumhj-W_l-i7kiIsImVtYWlsIjoibGVvLnllbkBwb3VjaGVuLmNvbSJ9.JFL6WhuaF7pz5Cc1kd2TlggsgVE1zjn29izyL-lE-bWGry5MvtXsFcgFUqkcbQKsKO-swgmvc9AYxazSO0SknydE_lmJd4XKCI7fNg2Uhy_CPwIRprIMbeAqdwWEbiimJiC6ARGuMLPpuWuCOAZsm9iKAo2bAGMx-rR3odXbQgVNG4xlw2awhPPhCtdrzJY-RYY9DINWSDzNk1qyms49vHu21O6njA6X4jsLNGLcT9zMndkioqYBC-fVeihXC0zfcRrM4V4VEBST2b2Yom5HvbcAfB5tNdgEPBqpi6Fonijc0618wm5WGzOfhr6oB-0yQmGgVSUdPRc7aUFUALmQUg";
            var key = "c848a486-8923-46c7-b1c1-6312667558cd";

            var data = JsonWebToken.Decode(token, key);

            Assert.AreEqual(1634546125, data.exp);
            Assert.AreEqual(1634517325, data.iat);
            Assert.AreEqual(1634517303, data.auth_time);
            Assert.AreEqual("4b573b00-f1fa-43d7-912f-7129b740b6df", data.jti);
            Assert.AreEqual("https://iam.pouchen.com/auth/realms/pcg", data.iss);
            Assert.AreEqual("pcg-scm-client", data.aud);
            Assert.AreEqual("d76155b6-c155-494d-bcb3-88e176fbc22c", data.sub);
            Assert.AreEqual("ID", data.typ);
            Assert.AreEqual("pcg-scm-client", data.azp);
            Assert.AreEqual("fa461a8d-87cc-45df-8548-c7f00df601a2", data.session_state);
            Assert.AreEqual("rZZ8fTci6okrXuzoxRWFwg", data.at_hash);
            Assert.AreEqual("0", data.acr);
            Assert.AreEqual("20190200001344", data.pccuid);
            Assert.AreEqual("leo.yen", data.uid);
            Assert.AreEqual("01-集團員工", data.employee_type);
            Assert.AreEqual(false, data.email_verified);
            Assert.AreEqual("Leo Yen 顏志軒", data.name);
            Assert.AreEqual("leo.yen", data.preferred_username);
            Assert.AreEqual("顏志軒", data.given_name);
            Assert.AreEqual("顏志軒", data.family_name);
            Assert.AreEqual("leo.yen@pouchen.com", data.email);
            
        }

        /// <summary>
        /// Test JWT library decode & verify signature.
        /// </summary>
        [TestMethod]
        public void TestMethod3()
        {

            var token = "eyJhbGciOiJSUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICJCRWhKU082c202RXM5MDc5Qnp3dTBuR3JNcUpXNXFmWENDa0RCUnVPU3JRIn0.eyJleHAiOjE2MzQ2MTA3MTgsImlhdCI6MTYzNDYwNzExOCwiYXV0aF90aW1lIjoxNjM0NjA3MTE3LCJqdGkiOiIxOTAwOTdhNS05OTA4LTQ3NjgtYWE0Zi0zMzAyYTRhMDM1YjAiLCJpc3MiOiJodHRwczovL2lhbWxhYi5wb3VjaGVuLmNvbS9hdXRoL3JlYWxtcy9wY2ciLCJhdWQiOiJhY2NvdW50Iiwic3ViIjoiNTBiZDk3MzYtZjQzMi00YWY1LWFkZjUtY2RiYTk4YTgwNDkxIiwidHlwIjoiQmVhcmVyIiwiYXpwIjoicGNnLW9pZGMtZGVtby1jbGllbnQiLCJzZXNzaW9uX3N0YXRlIjoiMzkzNTI5ODktNTRjMC00OWM2LTk2MzUtMmM5OWM1MGNkNWZjIiwiYWNyIjoiMSIsImFsbG93ZWQtb3JpZ2lucyI6WyJodHRwOi8vbG9jYWxob3N0OjgwODAiLCJodHRwOi8vMTI3LjAuMC4xOjgwODAiXSwicmVhbG1fYWNjZXNzIjp7InJvbGVzIjpbImRlZmF1bHQtcm9sZXMtcGNnIiwib2ZmbGluZV9hY2Nlc3MiLCJ1bWFfYXV0aG9yaXphdGlvbiJdfSwicmVzb3VyY2VfYWNjZXNzIjp7ImFjY291bnQiOnsicm9sZXMiOlsibWFuYWdlLWFjY291bnQiLCJtYW5hZ2UtYWNjb3VudC1saW5rcyIsInZpZXctcHJvZmlsZSJdfX0sInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwiLCJwY2N1aWQiOiIyMDE5MDIwMDAwMTM0NCIsInVpZCI6Imxlby55ZW4iLCJlbXBsb3llZV90eXBlIjoiMDEt6ZuG5ZyY5ZOh5belIiwiZW1haWxfdmVyaWZpZWQiOmZhbHNlLCJuYW1lIjoiTGVvIFllbiDpoY_lv5fou5IiLCJwcmVmZXJyZWRfdXNlcm5hbWUiOiJsZW8ueWVuIiwiZ2l2ZW5fbmFtZSI6Iumhj-W_l-i7kiIsImZhbWlseV9uYW1lIjoi6aGP5b-X6LuSIiwiZW1haWwiOiJsZW8ueWVuQHBvdWNoZW4uY29tIn0.EFDXvm5bI6ssG7hCJMOUZmrueUt6OTzJmCxc2mzdAYu1__4jQwHx_EnGmCgfEOrQSLaH8CPxSDCYOMhziFYQVSMydkCb00dVI_IHKC-8nVeF823q_Tj3L5rJ5pgKAqplQHUiZ9a3S86Wne0qn7kQjw0CHKZoUiUBdjrQ5ek4TRLFlf6A2IVqLgKZFBRU41GMD_dyZzojFlRhPTaALa0RjfZoL9yWrrbIokUsBKsCUr8bbpF3yiz9pWE0_2prM2dxshd3uoeu_1Kgj0bQtAsr6V1TPTNh1rvZkDOlLPE1YGn_RP5nlZP_6vDyD7mTgIai3NFFi5NMoPiDv1Vd8aMlfw";
            
            var urlEncoder = new JwtBase64UrlEncoder();

            var rsaKey = RSA.Create();
            rsaKey.ImportParameters(new RSAParameters()
            {
                Modulus = urlEncoder.Decode("qNq7rb4jfWIrPQOUWewbqV5Ty8sSGvIyR1R8Y78q8hB8Zw5LYQZutw88vHd6gFGkiQOQ_xTZvUF4UDUM2MEGAjUoLiPr-AVQzMDQ7StyttFl-iLmNjkhOcOBsoscL-vNZKgWCJCl9fGYJy7h8xcdGjG6382AK5sew8FlDoFLZmnztdmE_RyFhKHwHsiRh_hmHKtRVu2h5JwFIje3TL0HWzrsfaqcmuO1DkdNadKKWDCc3htLpdmQUgQpo4t9HW90vfF516mP6uwRRVkYtvDnctYedbFqaVks6n7P_tLAG9hHhUcxKFVc_Mv_wcLRRZZBPSDos5m9-D2bpCywHP0oDQ"),
                Exponent = urlEncoder.Decode("AQAB")
            });

            var claims = new JwtBuilder()
                .WithAlgorithm(new RS256Algorithm(rsaKey))
                .MustVerifySignature()
                .Decode<IDictionary<string, object>>(token);

        }

        /// <summary>
        /// Test GetCertificate
        /// </summary>
        [TestMethod]
        public void TestMethod4()
        {
            var client =new PcgClient("pcg-oidc-demo-client", "", "https://iamlab.pouchen.com/auth/realms/pcg");
            var certRes = client.GetCertificate();
            Assert.AreEqual("qNq7rb4jfWIrPQOUWewbqV5Ty8sSGvIyR1R8Y78q8hB8Zw5LYQZutw88vHd6gFGkiQOQ_xTZvUF4UDUM2MEGAjUoLiPr-AVQzMDQ7StyttFl-iLmNjkhOcOBsoscL-vNZKgWCJCl9fGYJy7h8xcdGjG6382AK5sew8FlDoFLZmnztdmE_RyFhKHwHsiRh_hmHKtRVu2h5JwFIje3TL0HWzrsfaqcmuO1DkdNadKKWDCc3htLpdmQUgQpo4t9HW90vfF516mP6uwRRVkYtvDnctYedbFqaVks6n7P_tLAG9hHhUcxKFVc_Mv_wcLRRZZBPSDos5m9-D2bpCywHP0oDQ", certRes.keys[0].n);
        }

        /// <summary>
        /// Test RefreshToken
        /// </summary>
        [TestMethod]
        public void TestMethod5()
        {
            var client =new PcgClient("pcg-oidc-demo-client", "", "https://iamlab.pouchen.com/auth/realms/pcg");
            var tokenRes = client.RefreshToken("eyJhbGciOiJIUzI1NiIsInR5cCIgOiAiSldUIiwia2lkIiA6ICI0MTM5OTBjOC1iNDA4LTQyYzQtOThhOC0yYTI0OGM3NjZjYzQifQ.eyJleHAiOjE2MzQ5MTY4NjcsImlhdCI6MTYzNDg3NzUzNSwianRpIjoiZTI2OTU2YWYtZDdjYy00YmUyLTk3MDQtYTgwMmJkMTg5ZDEwIiwiaXNzIjoiaHR0cHM6Ly9pYW1sYWIucG91Y2hlbi5jb20vYXV0aC9yZWFsbXMvcGNnIiwiYXVkIjoiaHR0cHM6Ly9pYW1sYWIucG91Y2hlbi5jb20vYXV0aC9yZWFsbXMvcGNnIiwic3ViIjoiNTBiZDk3MzYtZjQzMi00YWY1LWFkZjUtY2RiYTk4YTgwNDkxIiwidHlwIjoiUmVmcmVzaCIsImF6cCI6InBjZy1vaWRjLWRlbW8tY2xpZW50Iiwic2Vzc2lvbl9zdGF0ZSI6ImZhYTZkN2IwLWI1MTktNDJmNS1hYjYyLTdhMTMwMTE2NDY0ZiIsInNjb3BlIjoib3BlbmlkIHByb2ZpbGUgZW1haWwifQ.a4rpPIc1IyQigdGfYOYTM_ajHUGDl5jYATWo7mMVFm8");
            Assert.AreEqual("", tokenRes.refresh_token);
        }


        /// <summary>
        /// reference
        /// https://github.com/googlesamples/oauth-apps-for-windows/blob/3f14353db7085ea1927ae6bc9951f39bf6946ec7/OAuthConsoleApp/OAuthConsoleApp/Program.cs#L59
        /// </summary>
        [TestMethod]
        public void TestMethod6()
        {

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


            Assert.AreEqual("pcg-oidc-demo-client", userData["azp"]);
            Assert.AreEqual("https://iamlab.pouchen.com/auth/realms/pcg", userData["iss"]);
        }

        public int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }
    }

}
