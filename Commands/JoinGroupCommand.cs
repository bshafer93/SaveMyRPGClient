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
        public JoinGroupViewModel jcvm { get; set; }
        public JoinGroupCommand(JoinGroupViewModel vm)
        {
            jcvm = vm;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            UserModel um = new UserModel(Properties.Settings.Default.Username, Properties.Settings.Default.Email);

            bool didJoin = await App.Client.JoinCampaign(um, jcvm.ID);

            if (!didJoin)
            {
                Debug.WriteLine("Failed To Join group");
                return;
            }
            jcvm._clvm.addCampaign(jcvm.ID);
        }
    }
}
