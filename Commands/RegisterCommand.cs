using SaveMyRPGClient.Model;
using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Commands
{
    public class RegisterCommand: AsyncCommand
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
            ViewModel.ErrorMessage = "Contacting Server...";

            UserModel um = new Model.UserModel(ViewModel.Password, ViewModel.Email);

            bool isRegistered = await App.Client.Register(um);

            if (!isRegistered)
            {
                ViewModel.ErrorMessage = "Registration Failed";
                Debug.WriteLine("Registration Failed!");
                return;
            }
            ViewModel.ErrorMessage = "Registered!";

        }
    }
}

