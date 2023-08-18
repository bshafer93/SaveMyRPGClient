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

namespace SaveMyRPGClient.ViewModel
{
    public class LoginViewModel : ViewModelBase
    {
        private string _username;
        private string _email;

        private string _errorMessage;
        private bool _isViewVisible=true;

        public string Username
        {
            get
            {
                return _username;

            }
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }
        public string Email
        {
            get
            {
                return _email;

            }
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
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

        public LoginCommand VMLoginCommand { get; }
        public LoginViewModel()
        {
            VMLoginCommand = new LoginCommand(this);
        }

    }
}
