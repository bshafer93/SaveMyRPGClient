using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private bool _isViewVisible=true;

        public ViewModelBase CurrentSaveListViewModel { get; }
        public ViewModelBase CurrentCampaignListViewModel { get; }

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

        public MainViewModel() { 
            CurrentSaveListViewModel = new SaveListViewModel();
            CurrentCampaignListViewModel = new CampaignListViewModel();
        }

    }
}
