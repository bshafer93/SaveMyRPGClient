using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveMyRPGClient.Model;
namespace SaveMyRPGClient.ViewModel
{
    public class CampaignListViewModel:ViewModelBase
    {
        private readonly ObservableCollection<CampaignViewModel> _campaignList;

        public IEnumerable<CampaignViewModel> CampaignList => _campaignList;



        public CampaignListViewModel()
        {
            _campaignList = new ObservableCollection<CampaignViewModel>()
            {
                new CampaignViewModel(new GroupModel(
                    new Random().Next().ToString(),
                    "Hotstuffs",
                    "bshafer93@gmail.com",
                    "adamIsAlive@gmail.com",
                    "",
                    "",
                    "asdfghjk34567ugh"
                    )),

                new CampaignViewModel(new GroupModel(
                    new Random().Next().ToString(),
                    "GnomeLovin",
                    "adamIsAlive@gmail.com",
                    "bshafer93@gmail.com",
                    "groguboss@gmail.com",
                    "",
                    "adfa345fgb"
                    )),


                new CampaignViewModel(new GroupModel(
                    new Random().Next().ToString(),
                    "MrTooshiesQuest",
                    "jakrus34@gmail.com",
                    "bshafer93@gmail.com",
                    "",
                    "",
                    "asdfh5678adf"
                    ))

            };
        }
    }
}
