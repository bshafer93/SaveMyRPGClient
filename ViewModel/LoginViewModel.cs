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
        private bool _rememberUser;

        private UserModel _user;

        public bool RememberUser
        {
            get
            { 
                return Properties.Settings.Default.RememberLogin; 
            } 
            set 
            {
                Properties.Settings.Default.RememberLogin = value;
              OnPropertyChanged(nameof(Properties.Settings.Default.RememberLogin));
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
            RememberUser = Properties.Settings.Default.RememberLogin;

        }

        public void CheckUserSettingsLogin() {

            if (Properties.Settings.Default.RememberLogin && Properties.Settings.Default.JwtTokenString.Length > 0)
            {
                var task = Task.Run(() => App.Client.AuthenticateUserToken());
                task.Wait();
                if (task.Result)
                {
                    IsViewVisible = false;
                }
                else {
                    Properties.Settings.Default.RememberLogin = false;
                    ErrorMessage = "Login Session Expired. Please Login again.";
                }
            }
        }

    }
}
