using Microsoft.IdentityModel.Tokens;
using SaveMyRPGClient.Model;
using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
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

            GroupModel new_gm = await App.Client.CreateCampaign(gm);

            if (new_gm == null)
            {
                CreateGroupVM.ErrorMessage = "Failed To create campaign...";
                Debug.WriteLine("Failed To create campaign...");
                return;
            }

            CreateGroupVM.ErrorMessage = "Campaign Created!";



            string save_path = CreateGroupVM.SavePath;

            if (save_path.IsNullOrEmpty()) { return; }

            if (File.Exists(save_path))
            {
                Debug.WriteLine("Choose Save Folder and not file...");
                return;
            }


            DirectoryInfo dirInfo = new DirectoryInfo(save_path);

            FileInfo[] files = dirInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.Extension.EndsWith("lsv"))
                {
                    SaveModel save = new SaveModel();
                    string directory_name = dirInfo.Name;
                    string file01_name = file.Name;
                    save.Save_Owner = Properties.Settings.Default.Email;
                    save.Group_Id = new_gm.Id;
                    save.Hash = SHA256.HashData(File.ReadAllBytes(file.FullName)).ToString();
                    save.CDN_Path = new_gm.Id + "/" + directory_name + "/" + file01_name;
                    save.Date_Created = new FileInfo(file.FullName).CreationTime;

                    bool didUpload = await App.Client.UploadSaveFile(save.Group_Id, file.FullName, directory_name, file01_name, save.Save_Owner);

                    if (!didUpload)
                    {
                        Debug.WriteLine("Upload Failed");
                        return;
                    }

                }
                else
                {
                    bool didUpload = await App.Client.UploadSaveImage(new_gm.Id, file.FullName);

                    if (!didUpload)
                    {
                        Debug.WriteLine("Upload Failed");
                        return;
                    }

                }

            }

            CreateGroupVM.updateCampaignListView();
        }
    }
}
