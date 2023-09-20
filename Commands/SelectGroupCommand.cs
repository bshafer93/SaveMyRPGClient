using SaveMyRPGClient.ViewModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class SelectGroupCommand : AsyncCommand
    {
        public CampaignViewModel cvm { get; set; }
        public SelectGroupCommand(CampaignViewModel vm)
        {
            cvm = vm;

        }

        public override bool CanExecute()
        {
            return true;
        }


        public override async Task ExecuteAsync()
        {
            cvm._clvm.changeCampaignView(cvm.GroupID);

            Debug.WriteLine("Change to:" + cvm.Name);

        }

    }
}
