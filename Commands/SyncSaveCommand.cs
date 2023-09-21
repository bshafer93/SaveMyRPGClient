﻿using SaveMyRPGClient.ViewModel;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
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
            
            if (SaveListVM.SavesList.Count() > 0){
                SaveListVM.StatusMessage = "Uploading Comments...";
                foreach (var save in SaveListVM.SavesList)
                {
                    bool didUpdate = await App.Client.UpdateSaveComment(save._save);
                    if (didUpdate) { SaveListVM.StatusMessage = "Comment for " + save._save.Folder_Name + " Updated"; }
                }
                SaveListVM.StatusMessage = "Comments Uploaded";

            }



            SaveListVM.StatusMessage = "Syncing Save now...";
            var saves = await App.Client.RetrieveAllCampaignSaves(SaveListVM.GroupID);

            SaveListVM._saveList = new ObservableCollection<SaveViewModel>();

            if (saves == null) return;

            foreach (var save in saves)
            {
                SaveListVM._saveList.Add(new SaveViewModel(save, SaveListVM));
            }
            SaveListVM.StatusMessage = "Sync Save Successful";
        }
    }
}
