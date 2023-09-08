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
    public class LoginViewModel : ViewModelBase
    {

        private string _errorMessage;
        private bool _isViewVisible=true;
        private bool _rememberUser = false;

        private UserModel _user;

        public bool RememberUser
        {
            get
            { 
                return _rememberUser; 
            } 
            set 
            { 
                _rememberUser = value;
              OnPropertyChanged(nameof(RememberUser));
            }        
        }

        public string Password
        {
            get
            {
                return _user.Password;

            }
            set
            {
                _user.Password = value;
                OnPropertyChanged(nameof(Password));
            }
        }
        public string Email
        {
            get
            {
                return _user.Email;

            }
            set
            {
                _user.Email = value;
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
        public RegisterCommand VMRegisterCommand { get; }
        public LoginViewModel()
        {
            _user = new UserModel(Properties.Settings.Default.Username, Properties.Settings.Default.Email);

            VMLoginCommand = new LoginCommand(this);
            VMRegisterCommand = new RegisterCommand(this);

            Password = "";
            Email = Properties.Settings.Default.Email;

            if (Properties.Settings.Default.RememberLogin)
            { 
                //HERE TODO
            }
        }

    }
}
