using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using SaveMyRPGClient;
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

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel()
        {
            LoginCommand = new ViewModelCommand(ExecuteLoginCommand,CanExecuteLoginCommand);
            RegisterCommand = new ViewModelCommand(p => ExecuteRegisterCommand("",""), p=>CanExecuteRegisterCommand("", ""));
        }


        private bool CanExecuteLoginCommand(object obj)
        {
            bool validData;
            if (string.IsNullOrWhiteSpace(Username) || Username.Length < 3|| string.IsNullOrWhiteSpace(Email) || Username.Length < 4)
            {
                validData = false;
            }
            validData = true;
            return validData;
        }

        private async void ExecuteLoginCommand(object obj)
        {
            ErrorMessage = "Contacting Server...";
            bool isValidUser = await App.Client.AuthenticateUser(new Model.UserModel(Username, Email));

            ErrorMessage = "Sending...";

            if (isValidUser)
            {
                ErrorMessage = "Logged In!";
                IsViewVisible = false;
            }
            else {
                ErrorMessage = "Invalid Username or Email";
            }

        }

        private bool CanExecuteRegisterCommand(string username, string email)
        {
            throw new NotImplementedException();
        }

        private void ExecuteRegisterCommand(string username, string email)
        {
            throw new NotImplementedException();
        }
    }
}
