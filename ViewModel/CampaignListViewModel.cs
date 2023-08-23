using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SaveMyRPGClient.Commands;
using SaveMyRPGClient.Model;
namespace SaveMyRPGClient.ViewModel
{
    public class CampaignListViewModel:ViewModelBase
    {
        private readonly ObservableCollection<CampaignViewModel> _campaignList;

        public IEnumerable<CampaignViewModel> CampaignList => _campaignList;

        public JoinGroupCommand JoinGroupCMD { get; }

        public SelectGroupCommand SelectGroupCMD { get; }

        public SaveListViewModel currentSaveListViewModel { get; set; }

        public List<SaveListViewModel> SaveListViewModelList { get; set; }

        public CampaignListViewModel()
        {
            SaveListViewModelList = new List<SaveListViewModel>();
            var task = Task.Run(() => App.Client.RetrieveAllJoinedCampaigns(Properties.Settings.Default.Email));

            task.Wait();
            var groups = task.Result;

            _campaignList = new ObservableCollection<CampaignViewModel>();

            foreach (var group in groups)
            {
                _campaignList.Add(new CampaignViewModel(group));
                SaveListViewModelList.Add(new SaveListViewModel(group.Id));

            }
            if (SaveListViewModelList.IsNullOrEmpty()) {
                return;
            }
            currentSaveListViewModel = SaveListViewModelList[0];

        }
    }
}
