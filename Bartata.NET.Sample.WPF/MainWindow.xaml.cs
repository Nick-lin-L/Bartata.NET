using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
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

namespace Bartata.NET.Sample.WPF
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {

        PcgClient client = new PcgClient("pcg-oidc-demo-client", "", "https://iamlab.pouchen.com/auth/realms/pcg");

        public MainWindow()
        {
            InitializeComponent();
            InitializeAsync();

            webView.Source = new Uri(client.GetServiceLoginUrl(new Uri("http://localhost:8080")).AbsoluteUri);
                ;
        }

        private async void InitializeAsync()
        {
            await webView.EnsureCoreWebView2Async(null);
            webView.CoreWebView2.AddWebResourceRequestedFilter("*",CoreWebView2WebResourceContext.All);
            webView.CoreWebView2.WebResourceRequested+=CoreWebView2_WebResourceRequested;
        }

        private void CoreWebView2_WebResourceRequested(object sender, Microsoft.Web.WebView2.Core.CoreWebView2WebResourceRequestedEventArgs e)
        {
        }

        private void webView_Loaded(object sender, RoutedEventArgs e)
        {       
        }

        private void webView_NavigationStarting(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationStartingEventArgs e)
        {
            var a = webView.CoreWebView2;
            if (e.Uri.StartsWith("http://localhost:8080"))
            {
                NameValueCollection outer = HttpUtility.ParseQueryString(e.Uri);
                var code = outer["code"];
                var token = client.QeuryToken(new Uri("http://localhost:8080"), code, true);
                var userData = client.GetUserData(token.access_token, false);
                this.Title +=  " - " + userData["name"];
            }
        }

        private void webView_NavigationCompleted(object sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
        {
            var a = webView.CoreWebView2;

        }

        private void webView_SourceChanged(object sender, Microsoft.Web.WebView2.Core.CoreWebView2SourceChangedEventArgs e)
        {

        }

        private void webView_SourceUpdated(object sender, DataTransferEventArgs e)
        {

        }
    }
}
