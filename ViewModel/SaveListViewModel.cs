using SaveMyRPGClient.Commands;
using SaveMyRPGClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.ViewModel
{
    public class SaveListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<SaveViewModel> _saveList;
        public UploadSaveCommand UploadSaveCMD { get; set; }

        public SyncSaveCommand SyncSaveCMD { get; set; }
        public IEnumerable<SaveViewModel> SavesList => _saveList;

        public string GroupID { get;  set; }
        public string GroupName { get; set; }

        public SaveListViewModel() 
        {
            _saveList = new ObservableCollection<SaveViewModel>();
            UploadSaveCMD = new UploadSaveCommand(this);
        }

        public SaveListViewModel(string group_id, string group_name) {
            GroupID = group_id;
            UploadSaveCMD = new UploadSaveCommand(this);

            var task = Task.Run(() => App.Client.RetrieveAllCampaignSaves(group_id));
            task.Wait();
            var saves = task.Result;

            _saveList = new ObservableCollection<SaveViewModel>();

            foreach (var save in saves)
            {
                _saveList.Add(new SaveViewModel(save));
            }


        }


        public async Task<bool> SyncSaves() 
        {
            _saveList.Clear();
            var saves = await App.Client.RetrieveAllCampaignSaves(GroupID);

            foreach (var save in saves)
            {
                _saveList.Add(new SaveViewModel(save));
            }
            return true;
        }


    }


    
}
