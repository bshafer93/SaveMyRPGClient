using SaveMyRPGClient.Commands;

namespace SaveMyRPGClient.ViewModel
{
    public class SettingsViewModel : ViewModelBase
    {
        private string _errorMessage;
        private bool _isViewVisible = true;
        public SaveSettingsCommand SaveSettingsCMD { get; }
        public SettingsViewModel()
        {
            SaveSettingsCMD = new SaveSettingsCommand(this);
        }

        public string Email
        {
            get
            {
                return Properties.Settings.Default.Email;
            }
            set
            {
                Properties.Settings.Default.Email = value;
                OnPropertyChanged(nameof(Email));
            }
        }
        public bool RememberLogin
        {
            get
            {
                return Properties.Settings.Default.RememberLogin;
            }
            set
            {
                Properties.Settings.Default.RememberLogin = value;
                OnPropertyChanged(nameof(RememberLogin));
            }
        }

        public string SavePath
        {
            get
            {
                return Properties.Settings.Default.SavePath;
            }
            set
            {
                Properties.Settings.Default.SavePath = value;
                OnPropertyChanged(nameof(SavePath));
            }
        }
        public string JwtTokenString
        {
            get
            {
                return Properties.Settings.Default.JwtTokenString;
            }
            set
            {
                Properties.Settings.Default.JwtTokenString = value;
                OnPropertyChanged(nameof(JwtTokenString));
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
    }


}
