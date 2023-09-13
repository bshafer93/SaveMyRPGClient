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
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            string save_path = "";

            if (result.ToString() != string.Empty)
            {
                save_path = openFileDlg.SelectedPath;
                
            }

            if (save_path.Length < 1) { return; }

            if (File.Exists(save_path.ToString())) {
                Debug.WriteLine("Choose Save Folder and not file...");
                return;
            }

           bool didUpload = await App.Client.UploadSaveFolder(save_path, SaveListVM.GroupID);
           
            if(!didUpload)
            {
                Debug.WriteLine("Save Failed to Upload");
            }

        }
    }
}
