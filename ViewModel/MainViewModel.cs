using SaveMyRPGClient.Commands;

namespace SaveMyRPGClient.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        private bool _isViewVisible = true;

        public CampaignListViewModel CurrentCampaignListViewModel { get; }
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
