using SaveMyRPGClient.ViewModel;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class UploadSaveCommand : AsyncCommand
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
            SaveListVM.StatusMessage = "Uploading Save...";
            System.Windows.Forms.FolderBrowserDialog openFileDlg = new System.Windows.Forms.FolderBrowserDialog();
            var result = openFileDlg.ShowDialog();
            string save_path = "";

            if (result.ToString() != string.Empty)
            {
                save_path = openFileDlg.SelectedPath;

            }

            if (save_path.Length < 1) { return; }

            if (File.Exists(save_path.ToString()))
            {
                Debug.WriteLine("Choose Save Folder and not file...");
                SaveListVM.StatusMessage = "Choose Save Folder and not file...";
                return;
            }

            bool didUpload = await App.Client.UploadSaveFolder(save_path, SaveListVM.GroupID);

            if (!didUpload)
            {
                Debug.WriteLine("Save Failed to Upload");
                SaveListVM.StatusMessage = "Failed to upload save...";
            }
            SaveListVM.StatusMessage = "Upload Successful!";
        }
    }
}
