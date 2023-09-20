using SaveMyRPGClient.ViewModel;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands

{
    using Microsoft.IdentityModel.Tokens;
    using SaveMyRPGClient.Model;
    public class LoginCommand : AsyncCommand
    {
        public LoginViewModel ViewModel { get; set; }
        public LoginCommand(LoginViewModel vm)
        {
            ViewModel = vm;
        }

        public override bool CanExecute()
        {

            return true;
        }

        public override async Task ExecuteAsync()
        {
            if (Properties.Settings.Default.RememberLogin && Properties.Settings.Default.JwtTokenString.IsNullOrEmpty())
            {
                ViewModel.ErrorMessage = "Invalid Token Please Login";
                Properties.Settings.Default.RememberLogin = false;
                Properties.Settings.Default.Save();
                return;
            }

            if (ViewModel.Email.IsNullOrEmpty())
            {
                ViewModel.ErrorMessage = "Email field is empty...";
                return;
            }

            if (ViewModel.Password.IsNullOrEmpty())
            {
                ViewModel.ErrorMessage = "Password field is empty...";
                return;
            }

            ViewModel.ErrorMessage = "Contacting Server...";

            Properties.Settings.Default.RememberLogin = ViewModel.RememberUser;
            Properties.Settings.Default.JwtTokenString = App.Client.TokenSignature;
            Properties.Settings.Default.Email = ViewModel.Email;
            Properties.Settings.Default.Save();

            UserModel um = new Model.UserModel(ViewModel.Password, ViewModel.Email);

            bool isAuthenticated = await App.Client.AuthenticateUser(um);


            if (!isAuthenticated)
            {
                ViewModel.ErrorMessage = "Invalid Username or Email";
                return;
            }

            ViewModel.ErrorMessage = "Logged In!";
            ViewModel.IsViewVisible = false;

        }
    }
}
