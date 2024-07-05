using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Bartata.NET.Sample.WinForm
{
    public partial class Form1 : Form
    {
        string key = "11001";

        public Form1()
        {
            InitializeComponent();

            var appName = Process.GetCurrentProcess().ProcessName + ".exe";

            RegistryKey key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            if (key != null)
            {
                key.SetValue(appName, 11001, RegistryValueKind.DWord);
#if DEBUG
                key.SetValue(Process.GetCurrentProcess().ProcessName + ".vshost.exe", 11001, RegistryValueKind.DWord);//调试运行需要加上，否则不起作用
#endif
            }

            key = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Wow6432Node\Microsoft\Internet Explorer\MAIN\FeatureControl\FEATURE_BROWSER_EMULATION", true);
            if (key != null)
            {
                key.SetValue(appName, 11001, RegistryValueKind.DWord);
#if DEBUG
                key.SetValue(Process.GetCurrentProcess().ProcessName + ".vshost.exe", 11001, RegistryValueKind.DWord);//调试运行需要加上，否则不起作用
#endif
            }
            webBrowser1.Url = new Uri("https://iamlab.pouchen.com/auth/realms/pcg/protocol/openid-connect/auth?client_id=pcg-oidc-demo-client&scope=openid&response_type=code&redirect_uri=http%3A%2F%2Flocalhost%3A8080");
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
