using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation;

namespace SaveMyRPGClient.Commands
{
    public class SelectGroupCommand : AsyncCommand
    {
        public CampaignViewModel ViewModel { get; set; }
        public CampaignListViewModel ListViewModel { get; set; }
        public SelectGroupCommand(CampaignViewModel vm)
        {
            ViewModel = vm;
            
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override Task ExecuteAsync()
        {
            return Task.CompletedTask;
            
        }

    }
}
