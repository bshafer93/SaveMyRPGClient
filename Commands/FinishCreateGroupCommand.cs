using Microsoft.IdentityModel.Tokens;
using SaveMyRPGClient.Model;
using SaveMyRPGClient.ViewModel;
using System.IO;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class FinishCreateGroupCommand : AsyncCommand
    {
        public CreateGroupViewModel CreateGroupVM { get; set; }
        public FinishCreateGroupCommand(CreateGroupViewModel cgvm)
        {
            CreateGroupVM = cgvm;
        }

        public override bool CanExecute()
        {

            if (CreateGroupVM.SavePath.IsNullOrEmpty())
            {
                CreateGroupVM.ErrorMessage = "Save path is empty.";
                return false;
            }

            if (CreateGroupVM.Name.IsNullOrEmpty())
            {
                CreateGroupVM.ErrorMessage = "Group name is empty.";
                return false;
            }

            if (File.Exists(CreateGroupVM.SavePath))
            {
                CreateGroupVM.ErrorMessage = "Choose Save folder not file.";
                return false;
            }

            return true;
        }

        public override async Task ExecuteAsync()
        {
            GroupModel gm = new GroupModel();
            gm.Name = CreateGroupVM.Name;
            gm.Host_Email = CreateGroupVM.HostEmail;

            GroupModel? new_gm = await App.Client.CreateCampaign(gm);

            if (new_gm == null)
            {
                CreateGroupVM.ErrorMessage = "Failed To create campaign...";
                return;
            }

            CreateGroupVM.ErrorMessage = "Campaign Created!";

            if (!await App.Client.UploadSaveFolder(CreateGroupVM.SavePath, new_gm.Id))
            {
                CreateGroupVM.ErrorMessage = "Save Upload failed.";
                return;
            }

            CreateGroupVM.ErrorMessage = "Campaign Created! & Save Uploaded!";

            await CreateGroupVM.updateCampaignListView();
        }
    }
}
