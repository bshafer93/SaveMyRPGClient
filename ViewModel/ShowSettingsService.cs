using SaveMyRPGClient.View;

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
