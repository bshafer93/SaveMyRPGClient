using SaveMyRPGClient.Commands;
using SaveMyRPGClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SaveMyRPGClient.ViewModel
{
    public class JoinGroupViewModel : ViewModelBase
    {

        private string _id;
        public CampaignListViewModel _clvm;
        public JoinGroupCommand JoinGroupCMD {get;}
        private string _errorMessage;
        private bool _isViewVisible = true;
        public JoinGroupViewModel(CampaignListViewModel clvm) {
      
            _clvm = clvm;
            JoinGroupCMD = new JoinGroupCommand(this);
        
        }

        public string ErrorMessage
        {
            get
            {
                return _errorMessage;

            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

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

        public string ID
        {
            get
            {
                return _id;

            }
            set
            {
                _id = value;
                OnPropertyChanged(nameof(ID));
            }
        }
    }
}
