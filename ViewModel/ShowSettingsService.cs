using SaveMyRPGClient.View;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.ViewModel
{
    public class ShowSettingsService
    {
        public ShowSettingsService()
        {

        }
        public void ShowDialog()
        {
            var settingsView = new SettingsView();
            settingsView.DataContext = new SettingsViewModel();
            settingsView.ShowDialog();
        }
    }

}
