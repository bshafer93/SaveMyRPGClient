using SaveMyRPGClient.View;
using SaveMyRPGClient.ViewModel;
using System.IO;
using System.Windows;
using System.Windows.Threading;

namespace SaveMyRPGClient
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static SMRPGClient client = new SMRPGClient();

        public static SMRPGClient Client { get => client; set => client = value; }

        public void ApplicationStart(object sender, StartupEventArgs e)
        {
            this.DispatcherUnhandledException += new DispatcherUnhandledExceptionEventHandler(Application_DispatcherUnhandledException);
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
            System.Windows.MessageBox.Show(string.Format("An error occured: {0}", e.Exception.Message), "Error");
            e.Handled = true;
            
        }
    }
}
