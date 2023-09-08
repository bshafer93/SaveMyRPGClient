using SaveMyRPGClient.View;
using SaveMyRPGClient.ViewModel;
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

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            //SetUpClient
            Client = new SMRPGClient();


            LoginView loginView = new LoginView();
            LoginViewModel loginVM = new LoginViewModel();
            loginView.DataContext = loginVM;

            loginView.Show();
            loginView.IsVisibleChanged += (sender, e) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {
                    MainView mainView = new MainView();
                    MainViewModel mainViewModel = new MainViewModel();
                    mainView.DataContext = mainViewModel;
                    loginView.Close();
                    mainView.Show();
                }

            };

            loginVM.CheckUserSettingsLogin();

        }
    }
}
