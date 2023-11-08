using SaveMyRPGClient.Commands;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace SaveMyRPGClient.ViewModel
{
    public class SaveListViewModel : ViewModelBase
    {
        public  ObservableCollection<SaveViewModel> _saveList;
        public UploadSaveCommand UploadSaveCMD { get; set; }
        public SyncSaveCommand SyncSavesCMD { get; set; }

        private string _statusMessage;

        public string StatusMessage
        {
            get
            {
                return _statusMessage;
            }
            set
            {
                _statusMessage = value;
                OnPropertyChanged(nameof(StatusMessage));
            }
        
        }

        public IEnumerable<SaveViewModel> SavesList => _saveList;


        public string GroupIDLabel 
        { 
            get 
            { 
                return "Group ID"; 
            } 
            set
            { 

            } 
        }
        public string GroupID { get; set; }
        public string GroupName { get; set; }

        public SaveListViewModel(string group_id, string group_name, CampaignViewModel cvm)
        {
            GroupID = group_id;
            UploadSaveCMD = new UploadSaveCommand(this,cvm._clvm);
            SyncSavesCMD = new SyncSaveCommand(this);
            GroupName = group_name;
            RefreshSaveList();

        }

        public void RefreshSaveList()
        {
            var task = Task.Run(() => App.Client.RetrieveAllCampaignSaves(GroupID));
            task.Wait();
            var saves = task.Result;

            _saveList = new ObservableCollection<SaveViewModel>();

            if (saves == null) return;

            foreach (var save in saves)
            {
                _saveList.Add(new SaveViewModel(save, this));

                if (_saveList.Last().IsLocal)
                {
                    Debug.WriteLine(save.Folder_Name + " Is saved locally");
                }
            }


        }

    }



}
