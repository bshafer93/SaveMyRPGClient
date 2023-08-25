using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class ShowJoinGroupCommand : AsyncCommand
    {
        public CampaignListViewModel clvm { get; set; }
        public ShowJoinGroupCommand(CampaignListViewModel vm)
        {
            clvm = vm;

        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {

            clvm.ShowJoinGroupSRV.ShowDialog();

        }
    }
}
