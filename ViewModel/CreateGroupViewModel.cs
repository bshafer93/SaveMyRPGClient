using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SaveMyRPGClient;
using SaveMyRPGClient.Commands;
using System.Diagnostics;
using Windows.Graphics.Printing.Workflow;
using System.Windows.Threading;
using SaveMyRPGClient.Model;

namespace SaveMyRPGClient.ViewModel
{
    public class CreateGroupViewModel : ViewModelBase
    {

        private GroupModel _group;
        private string _save_path;
        private string _errorMessage;
        private bool _isViewVisible = true;
        private CampaignListViewModel _clvm;
        public FinishCreateGroupCommand FinishCreateGroupCMD { get; }
        public OpenFolderDialogCommand OpenFolderDialogCMD { get; }
        public CampaignListViewModel CampaignListVM {
            get 
            {
                return _clvm;
            }
        }
        public string Name
        {
            get
            {
                return _group.Name;

            }
            set
            {
                _group.Name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        public string HostEmail
        {
            get
            {
                return _group.Host_Email;

            }
            set
            {
                _group.Host_Email = value;
                OnPropertyChanged(nameof(HostEmail));
            }
        }

        public string SavePath
        {
            get
            {
                return _save_path;

            }
            set
            {
                _save_path = value;
                OnPropertyChanged(nameof(SavePath));
            }
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

        public void updateCampaignListView() {
            _clvm.updateCampaignView();
        }

        public CreateGroupViewModel(CampaignListViewModel clvm)
        {
            OpenFolderDialogCMD = new OpenFolderDialogCommand(this);
            _clvm = clvm;
            _errorMessage = "";
            _group = new GroupModel();
            _save_path = "";
            HostEmail = Properties.Settings.Default.Email;
            FinishCreateGroupCMD = new FinishCreateGroupCommand(this);
        }
    }
}
