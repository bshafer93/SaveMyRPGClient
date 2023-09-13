using Microsoft.IdentityModel.Tokens;
using SaveMyRPGClient.Commands;
using SaveMyRPGClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Capture;

namespace SaveMyRPGClient.ViewModel
{
    public class SaveListViewModel : ViewModelBase
    {
        private readonly ObservableCollection<SaveViewModel> _saveList;
        public UploadSaveCommand UploadSaveCMD { get; set; }

        public IEnumerable<SaveViewModel> SavesList => _saveList;

        public string GroupID { get;  set; }
        public string GroupName { get; set; }

        public SaveListViewModel(string group_id, string group_name) {
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

                if (_saveList.Last().IsLocal) {
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
