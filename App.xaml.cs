using SaveMyRPGClient.View;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Windows.ApplicationModel.UserDataTasks.DataProvider;

namespace SaveMyRPGClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static SMRPGClient client;

        public static SMRPGClient Client { get => client; set => client = value; }

        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            //SetUpClient
            Client = new SMRPGClient();

            LoginView loginView = new LoginView();
            loginView.Show();
            loginView.IsVisibleChanged += (sender, e) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    var mainView = new MainView();
                    mainView.Show();
                    loginView.Close();
                }

            };
        
        
        }
    }
}
