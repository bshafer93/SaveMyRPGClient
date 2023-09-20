using Microsoft.IdentityModel.Tokens;
using SaveMyRPGClient.Model;
using SaveMyRPGClient.ViewModel;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class RegisterCommand : AsyncCommand
    {
        public LoginViewModel ViewModel { get; set; }
        public RegisterCommand(LoginViewModel vm)
        {
            ViewModel = vm;
        }

        public override bool CanExecute()
        {


            return true;
        }

        public override async Task ExecuteAsync()
        {
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

            if (ViewModel.Password.Length <= 3)
            {
                ViewModel.ErrorMessage = "Password needs to be more than 3 characters...";
                return;
            }
            if (ViewModel.Password.Length >= 128)
            {
                ViewModel.ErrorMessage = "Password cannot be more than 128 characters";
                return;
            }

            ViewModel.ErrorMessage = "Contacting Server...";

            UserModel um = new Model.UserModel(ViewModel.Password, ViewModel.Email);

            bool isRegistered = await App.Client.Register(um);

            if (!isRegistered)
            {
                ViewModel.ErrorMessage = "Registration Failed";
                return;
            }
            ViewModel.ErrorMessage = "Registered!";

        }
    }
}

