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

        protected void ApplicationStart(object sender, StartupEventArgs e)
        {
            //SetUpClient
            Client = new SMRPGClient();

            MainView mainView = new MainView();
            LoginView loginView = new LoginView();

            loginView.Close();
            MainViewModel mainViewModel = new MainViewModel();
            mainView.DataContext = mainViewModel;
            mainView.Show();


            /*
            createGroupView.DataContext = new CreateGroupViewModel(mainViewModel.CurrentCampaignListViewModel);

            createGroupView.IsVisibleChanged += (sender, e) =>
            {
                if (createGroupView.IsVisible == false)
                {
                    createGroupView.Show();

                }
            };
            */

            /*
            loginView.Show();
            loginView.IsVisibleChanged += (sender, e) =>
            {
                if (loginView.IsVisible == false && loginView.IsLoaded)
                {

                    loginView.Close();
                    mainView.Show();
                }

            };
            */



        }
    }
}
