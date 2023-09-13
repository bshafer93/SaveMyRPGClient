using SaveMyRPGClient.View;
using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Threading;
using Windows.ApplicationModel.UserDataTasks.DataProvider;

namespace SaveMyRPGClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static SMRPGClient client= new SMRPGClient();

        public static SMRPGClient Client { get => client; set => client = value; }

        public void ApplicationStart(object sender, StartupEventArgs e)
        {

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
        private void Application_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            /*
            var exception = e.Exception;
            using (StreamWriter outputFile = new StreamWriter("C:\\Users\\brent\\Desktop\\smrpg_log.txt"))
            {
                    outputFile.WriteLine(exception.ToString());
            }
            */
        }
    }
}
