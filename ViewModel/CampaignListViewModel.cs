using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Tokens;
using SaveMyRPGClient.Commands;
using SaveMyRPGClient.Model;
using Windows.Media.Capture;
using Windows.System;

namespace SaveMyRPGClient.ViewModel
{
    public class CampaignListViewModel:ViewModelBase
    {
        private readonly ObservableCollection<CampaignViewModel> _campaignList;

        private SaveListViewModel _currentSLVM;
        private string _join_group_id;

        DialogService _dialogService;
        ShowJoinGroupService _joinGroupService;


        public DialogService DialogCreateGroup 
        {
            get 
            {
                return _dialogService;
            }
            set 
            { 
                _dialogService = value; 
            }
        
        }

        public ShowJoinGroupService ShowJoinGroupSRV
        {
            get
            {
                return _joinGroupService;
            }
            set
            {
                _joinGroupService = value;
            }
        }

        public IEnumerable<CampaignViewModel> CampaignList => _campaignList;

        public ShowJoinGroupCommand ShowJoinGroupCMD { get; }
        public ShowCreateGroupCommand CreateGroupCMD { get; }

        public SaveListViewModel CurrentSaveListViewModel
        { 
            get 
            {
                return _currentSLVM;
            }
            set 
            {
                _currentSLVM = value;
                OnPropertyChanged(nameof(CurrentSaveListViewModel));
            }
        }


        public List<SaveListViewModel> SaveListViewModelList { get; set; }



        public CampaignListViewModel()
        {
            _dialogService = new DialogService(this);
            _joinGroupService = new ShowJoinGroupService(this);
            ShowJoinGroupCMD = new ShowJoinGroupCommand(this);

            CreateGroupCMD = new ShowCreateGroupCommand(this);
            SaveListViewModelList = new List<SaveListViewModel>();
            

            var task = Task.Run(() => App.Client.RetrieveAllJoinedCampaigns(Properties.Settings.Default.Email));
            task.Wait();
            var groups = task.Result;

            _campaignList = new ObservableCollection<CampaignViewModel>();

            foreach (var group in groups)
            {
                _campaignList.Add(new CampaignViewModel(group,this));

                SaveListViewModelList.Add(new SaveListViewModel(group.Id,group.Name));

            }
            if (SaveListViewModelList.IsNullOrEmpty()) {
                return;
            }
            CurrentSaveListViewModel = SaveListViewModelList[0];

        }

        public void updateCampaignView() {

            var task = Task.Run(() => App.Client.RetrieveAllJoinedCampaigns(Properties.Settings.Default.Email));
            task.Wait();
            var groups = task.Result;

            _campaignList.Clear();
            SaveListViewModelList.Clear();

            foreach (var group in groups)
            {
                _campaignList.Add(new CampaignViewModel(group, this));

                SaveListViewModelList.Add(new SaveListViewModel(group.Id, group.Name));

            }
            if (SaveListViewModelList.IsNullOrEmpty())
            {
                return;
            }
            CurrentSaveListViewModel = SaveListViewModelList[0];

        }

        public void changeCampaignView(string group_id) 
        {
            CurrentSaveListViewModel = SaveListViewModelList.Find((SaveListViewModel a) => a.GroupID == group_id);
        }

        public void addCampaign(string group_id)
        {
            var task = Task.Run(() => App.Client.RetrieveCampaignInfo(group_id));
            task.Wait();
            var group = task.Result;

            _campaignList.Add(new CampaignViewModel(group, this));
            SaveListViewModelList.Add(new SaveListViewModel(group.Id, group.Name));
        }
    }
}
