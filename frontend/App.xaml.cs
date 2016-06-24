using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace AllyTalksClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            if (ConfigurationManager.AppSettings["login"] == string.Empty && ConfigurationManager.AppSettings["password"] == string.Empty)
            {
                StartupUri = new Uri("View/StartPage.xaml", UriKind.Relative);
            }
            else 
            {
                StartupUri = new Uri("View/MainWindow.xaml", UriKind.Relative);
            }
        }
    }
}
