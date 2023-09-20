using Microsoft.IdentityModel.Tokens;
using SaveMyRPGClient.Model;
using SaveMyRPGClient.ViewModel;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class JoinGroupCommand : AsyncCommand
    {
        public JoinGroupViewModel jcvm { get; set; }
        public JoinGroupCommand(JoinGroupViewModel vm)
        {
            jcvm = vm;
        }

        public override bool CanExecute()
        {
            if (jcvm.ID.IsNullOrEmpty())
            {
                jcvm.ErrorMessage = "Group ID field is empty.";
                return false;

            }

            return true;
        }

        public override async Task ExecuteAsync()
        {
            UserModel um = new UserModel(Properties.Settings.Default.Username, Properties.Settings.Default.Email);

            if (!await App.Client.JoinCampaign(um, jcvm.ID))
            {
                jcvm.ErrorMessage = "Failed To Join group";
                Debug.WriteLine("Failed To Join group");
            }
            else
            {
                jcvm.ErrorMessage = "Joined group successfully";
                await jcvm._clvm.addCampaign(jcvm.ID);
            }


        }
    }
}
