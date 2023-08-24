using SaveMyRPGClient.Model;
using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class FinishCreateGroupCommand: AsyncCommand
    {
        public CreateGroupViewModel CreateGroupVM { get; set; }
        public FinishCreateGroupCommand(CreateGroupViewModel cgvm)
        {
            CreateGroupVM = cgvm;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            GroupModel gm = new GroupModel();
            gm.Name = CreateGroupVM.Name;
            gm.Host_Email = CreateGroupVM.HostEmail;
            Debug.WriteLine("Save: " + CreateGroupVM.SavePath);

            GroupModel didCreate = await App.Client.CreateCampaign(gm);

            if (didCreate == null)
            {
                CreateGroupVM.ErrorMessage = "Failed To create campaign...";
                Debug.WriteLine("Failed To create campaign...");
                return;
            }

            CreateGroupVM.ErrorMessage = "Campaign Created!";

            bool didUpload = await App.Client.UploadSaveFile(didCreate, CreateGroupVM.SavePath);
            if (!didUpload) {
                CreateGroupVM.ErrorMessage = "Save Upload Failed";
                return;
            }
            CreateGroupVM.ErrorMessage = "Save Uploaded!";

        }
    }
}
