using Microsoft.IdentityModel.Tokens;
using SaveMyRPGClient.Commands;
using SaveMyRPGClient.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace SaveMyRPGClient.ViewModel
{
    public class CampaignListViewModel : ViewModelBase
    {
        static private ObservableCollection<CampaignViewModel> _campaignList;

        static private int _currentCampaignIndex = 0;

        private SaveListViewModel? _currentSaveListViewModel;

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

        public SaveListViewModel? CurrentSaveListViewModel
        {
            get
            {
                return _currentSaveListViewModel;
            }
            set
            {
                _currentSaveListViewModel = value;
                OnPropertyChanged(nameof(CurrentSaveListViewModel));
            }
 
        }

        public CampaignListViewModel()
        {
            _dialogService = new DialogService(this);
            _joinGroupService = new ShowJoinGroupService(this);
            ShowJoinGroupCMD = new ShowJoinGroupCommand(this);
            CreateGroupCMD = new ShowCreateGroupCommand(this);
            
           _campaignList = new ObservableCollection<CampaignViewModel>();


            var task = Task.Run(() => App.Client.RetrieveAllJoinedCampaigns(Properties.Settings.Default.Email));
            task.Wait();
            var groups = task.Result;
            if (groups == null)
            {
                return;
            }
            int i = 0;
            foreach (GroupModel group in groups)
            {
                _campaignList.Add(new CampaignViewModel(i,group, this));
                i++;
            }

            CurrentSaveListViewModel = (_campaignList.Count < 1) ? null : _campaignList[0].SaveListVM;

        }

        public async Task updateCampaignView()
        {

            var groups = await App.Client.RetrieveAllJoinedCampaigns(Properties.Settings.Default.Email);

            if (groups == null) return;

            _campaignList.Clear();

            int i = 0;
            foreach (GroupModel group in groups)
            {
                _campaignList.Add(new CampaignViewModel(i, group, this));
                i++;
            }

            CurrentSaveListViewModel = (_campaignList.Count < 1) ? null : _campaignList[0].SaveListVM;

        }

        public void changeCampaignView(string group_id)
        {

            int index = CampaignList.Single(i => i.GroupID == group_id).listIndex;
            _campaignList[index].updateSaveList();
            CurrentSaveListViewModel = _campaignList[index].SaveListVM;
        }

        public void changeCampaignView(CampaignViewModel group)
        {

            _campaignList[group.listIndex].updateSaveList();
            CurrentSaveListViewModel = _campaignList[group.listIndex].SaveListVM;
        }

        public async Task addCampaign(string group_id)
        {
            var group = await App.Client.RetrieveCampaignInfo(group_id);

            if (group == null) return;
            int index = _campaignList.Count - 1;
            _campaignList.Add(new CampaignViewModel(index,group, this));

        }
    }
}
