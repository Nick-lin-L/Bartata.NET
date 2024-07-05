using Bartata.NET;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Bartata.Sample.DeskApplication
{
    /// <summary>
    /// MainWindow.xaml 的互動邏輯
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            File.WriteAllText("D://.test", "test");

            try
            {
                var client = new PcgClient("pcg-oidc-demo-client", "", "https://iamlab.pouchen.com/auth/realms/pcg");

                var access_token = File.ReadAllText("D://.auth");

                var userData = client.GetUserData(access_token, false);

                userNameLabel.Content = userData["name"];
            }
            catch (Exception ex)
            {
                File.WriteAllText("D://.test", ex.Message);
            }

        }
    }
}
