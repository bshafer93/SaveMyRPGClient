using Microsoft.IdentityModel.Tokens;
using SaveMyRPGClient.Model;
using SaveMyRPGClient.ViewModel;
using System;
using System.IO;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Security.Cryptography;
using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace SaveMyRPGClient.Commands
{
    public class UploadSaveCommand:AsyncCommand
    {
        public SaveListViewModel SaveListVM { get; set; }
        public UploadSaveCommand(SaveListViewModel slvm)
        {
            SaveListVM = slvm;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            
            var folder_dialog = new CommonOpenFileDialog();
            folder_dialog.IsFolderPicker = true;
            CommonFileDialogResult save_path = folder_dialog.ShowDialog();

            if (save_path.ToString().Length < 1) { return; }

            if (File.Exists(save_path.ToString())) {
                Debug.WriteLine("Choose Save Folder and not file...");
                return;
            }


            DirectoryInfo dirInfo = new DirectoryInfo(save_path.ToString());

            FileInfo[] files = dirInfo.GetFiles();

            foreach (FileInfo file in files)
            {
                if (file.Extension.EndsWith("lsv"))
                {
                    SaveModel save = new SaveModel();
                    string directory_name = dirInfo.Name;
                    string file01_name = file.Name;
                    save.Save_Owner = Properties.Settings.Default.Email;
                    save.Group_Id = SaveListVM.GroupID;
                    save.Hash = SHA256.HashData(File.ReadAllBytes(file.FullName)).ToString();
                    save.CDN_Path = SaveListVM.GroupID + "/" + directory_name + "/" + file01_name;
                    save.Date_Created = new FileInfo(file.FullName).CreationTime;

                    bool didUpload = await App.Client.UploadSaveFile(save.Group_Id, file.FullName,directory_name,file01_name, save.Save_Owner);

                    if (!didUpload)
                    {
                        Debug.WriteLine("Upload Failed");
                        return;
                    }

                    bool didSync = await SaveListVM.SyncSaves();

                    if (!didSync)
                    {
                        Debug.WriteLine("Sync Failed");
                        return;
                    }

                }
                else
                {
                    bool didUpload = await App.Client.UploadSaveImage(SaveListVM.GroupID, file.FullName);

                    if (!didUpload)
                    {
                        Debug.WriteLine("Upload Failed");
                        return;
                    }

                }

            }


        }
    }
}
