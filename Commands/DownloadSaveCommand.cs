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
    public class DownloadSaveCommand: AsyncCommand
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

            await App.Client.DownloadSaveFile(SaveVM.FolderName, BaseSaveName + ".lsv", pullURLLSV);
            await App.Client.DownloadSaveFile(SaveVM.FolderName, BaseSaveName + ".WebP", pullURLImg);

            SaveVM.IsLocal = true;
        }
    }
}
