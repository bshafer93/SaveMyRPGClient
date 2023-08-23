using SaveMyRPGClient.Model;
using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class JoinGroupCommand : AsyncCommand
    {
        public CampaignListViewModel ViewModel { get; set; }
        public JoinGroupCommand(CampaignListViewModel vm)
        {
            ViewModel = vm;
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
