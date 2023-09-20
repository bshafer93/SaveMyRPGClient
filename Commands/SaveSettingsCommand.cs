using SaveMyRPGClient.ViewModel;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class SaveSettingsCommand : AsyncCommand
    {
        public SettingsViewModel ViewModel { get; set; }
        public SaveSettingsCommand(SettingsViewModel vm)
        {
            ViewModel = vm;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            Properties.Settings.Default.Save();
            ViewModel.ErrorMessage = "Settings Saved!";
        }
    }
}
