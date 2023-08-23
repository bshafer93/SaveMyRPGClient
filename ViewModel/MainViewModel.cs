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

        public CampaignListViewModel CurrentCampaignListViewModel { get; }

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
            CurrentCampaignListViewModel = new CampaignListViewModel();
        }

    }
}
