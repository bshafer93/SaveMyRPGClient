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
        public CampaignViewModel cvm { get; set; }
        public CreateGroupCommand(CampaignViewModel vm)
        {
            cvm = vm;

        }

        public override bool CanExecute()
        {
            return true;
        }


        public override async Task ExecuteAsync()
        {


            Debug.WriteLine("Change to:" + cvm.Name);

        }

    }
}
