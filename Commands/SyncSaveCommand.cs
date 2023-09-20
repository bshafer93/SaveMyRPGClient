using SaveMyRPGClient.ViewModel;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class SyncSaveCommand : AsyncCommand
    {
        public SaveListViewModel SaveListVM { get; set; }
        public SyncSaveCommand(SaveListViewModel slvm)
        {
            SaveListVM = slvm;
        }

        public override bool CanExecute()
        {
            return true;
        }

        public override async Task ExecuteAsync()
        {
            //bool didDownload = await App.Client.UploadSaveImage(SaveListVM.GroupID, file.FullName);
        }
    }
}
