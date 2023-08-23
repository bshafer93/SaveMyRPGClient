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

        public SaveListViewModel() 
        {
            _saveList = new ObservableCollection<SaveViewModel>
            {
                new SaveViewModel(new SaveModel(
                new Random().Next().ToString(),
                new Random().Next().ToString(),
                "bshafer93@gmail.com",
                "Norbertle_theHorse14",
                "myDoc/Norbertle_theHorse",
                DateTime.Now)),

                new SaveViewModel(new SaveModel(
                new Random().Next().ToString(),
                new Random().Next().ToString(),
                "bshafer93@gmail.com",
                "Norbertle_theHorse15",
                "myDoc/Norbertle_theHorse",
                DateTime.Now)),


                new SaveViewModel(new SaveModel(
                new Random().Next().ToString(),
                new Random().Next().ToString(),
                "bshafer93@gmail.com",
                "Norbertle_theHorsesd",
                "myDoc/Norbertle_theHorse",
                DateTime.Now))
            };
        }

        public SaveListViewModel(string group_id) {
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
