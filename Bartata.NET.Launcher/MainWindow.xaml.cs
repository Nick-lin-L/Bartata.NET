using Bartata.Launcher.Properties;
using Bartata.NET;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bartata.Launcher
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var client = new PcgClient(Settings.Default.appId, "", Settings.Default.realmHost);
            string redirectUri = Settings.Default.redirectUri;
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

            File.WriteAllText(Settings.Default.authFile, token.access_token);

            var p = new Process();
            p.StartInfo.FileName = Settings.Default.launchProgram;
            p.Start();

            
            Close();

        }
    }
}
