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
        private readonly ObservableCollection<SaveViewModel> _saveList;
        public UploadSaveCommand UploadSaveCMD { get; set; }

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

        public SaveListViewModel(string group_id, string group_name)
        {
            GroupID = group_id;
            UploadSaveCMD = new UploadSaveCommand(this);
            GroupName = group_name;

            var task = Task.Run(() => App.Client.RetrieveAllCampaignSaves(group_id));
            task.Wait();
            var saves = task.Result;

            _saveList = new ObservableCollection<SaveViewModel>();

            if (saves == null) return;

            foreach (var save in saves)
            {
                _saveList.Add(new SaveViewModel(save));

                if (_saveList.Last().IsLocal)
                {
                    Debug.WriteLine(save.Folder_Name + " Is saved locally");
                }
            }
        }


        public async Task<bool> SyncSaves()
        {
            _saveList.Clear();
            var saves = await App.Client.RetrieveAllCampaignSaves(GroupID);
            if (saves == null) return false;

            foreach (var save in saves)
            {
                _saveList.Add(new SaveViewModel(save));
            }
            return true;
        }


    }



}
