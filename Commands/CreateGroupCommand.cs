using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class CreateGroupCommand:AsyncCommand
    {
        public CampaignListViewModel clvm { get; set; }
        public CreateGroupCommand(CampaignListViewModel vm)
        {
            clvm = vm;

        }

        public override bool CanExecute()
        {
            return true;
        }


        public override async Task ExecuteAsync()
        {
            

        }

    }
}
