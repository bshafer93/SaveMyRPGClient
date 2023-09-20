using SaveMyRPGClient.ViewModel;
using System;
using System.Diagnostics;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class DownloadSaveCommand : AsyncCommand
    {
        public SaveViewModel SaveVM { get; set; }
        public DownloadSaveCommand(SaveViewModel slvm)
        {
            SaveVM = slvm;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            string BaseSaveName = SaveVM.FolderName.Split("__", StringSplitOptions.RemoveEmptyEntries)[1];

            string pullURLLSV = SaveVM.CDNPath.Replace("https://ny.storage.bunnycdn.com/savemyrpg", "https://savemyrpg.b-cdn.net");
            string pullURLImg = pullURLLSV.Replace(".lsv", ".WebP");

            bool save_ok = await App.Client.DownloadSaveFile(SaveVM.FolderName, BaseSaveName + ".lsv", pullURLLSV);
            bool img_ok = await App.Client.DownloadSaveFile(SaveVM.FolderName, BaseSaveName + ".WebP", pullURLImg);

            if (!save_ok || !img_ok)
            {
                Debug.WriteLine("Save failed to download.");
                SaveVM.IsLocal = false;
                return;
            }
            SaveVM.IsLocal = true;
        }
    }
}
