using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class ShowSettingsCommand: AsyncCommand
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
