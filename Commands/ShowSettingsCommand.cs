using SaveMyRPGClient.ViewModel;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class ShowSettingsCommand : AsyncCommand
    {

        public MainViewModel mvm { get; set; }
        public ShowSettingsCommand(MainViewModel vm)
        {
            mvm = vm;

        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            mvm._showSettingsService.ShowDialog();
        }
    }
}
