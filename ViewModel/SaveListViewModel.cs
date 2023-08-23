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
        
        public IEnumerable<SaveViewModel> SavesList => _saveList;

        public string GroupID { get;  set; }
        public string GroupName { get; set; }

        public SaveListViewModel() 
        {
            _saveList = new ObservableCollection<SaveViewModel>();
        }

        public SaveListViewModel(string group_id, string group_name) {
            GroupID = group_id;
            var task = Task.Run(() => App.Client.RetrieveAllCampaignSaves(group_id));
            _saveList = new ObservableCollection<SaveViewModel>();
            task.Wait();
            var saves = task.Result;

            _saveList = new ObservableCollection<SaveViewModel>();

            foreach (var save in saves)
            {
                _saveList.Add(new SaveViewModel(save));
            }

        }


    }


    
}
