using SaveMyRPGClient.Commands;

namespace SaveMyRPGClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isViewVisible = true;
        private CampaignListViewModel _campaignListViewModel;

        public CampaignListViewModel CurrentCampaignListViewModel {
            get
            {

                return _campaignListViewModel;
            }
            set
            {
                _campaignListViewModel = value;
                OnPropertyChanged(nameof(CurrentCampaignListViewModel));
            }
        }
        public ShowSettingsService _showSettingsService;
        public ShowSettingsCommand ShowSettingsCMD { get; }

        public bool IsViewVisible
        {
            get
            {
                return _isViewVisible;

            }
            set
            {
                _isViewVisible = value;
                OnPropertyChanged(nameof(IsViewVisible));
            }
        }

        public MainViewModel()
        {
            CurrentCampaignListViewModel = new CampaignListViewModel();
            ShowSettingsCMD = new ShowSettingsCommand(this);
            _showSettingsService = new ShowSettingsService();
        }

    }
}
