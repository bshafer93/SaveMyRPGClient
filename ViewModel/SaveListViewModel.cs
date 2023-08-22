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


    }
}
