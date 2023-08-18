using SaveMyRPGClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.Text.Json;
using System.Net.Http;
namespace SaveMyRPGClient.Commands

{
    using SaveMyRPGClient.Model;
    using System.Text.Json;
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
            bool isValidUser = false;
            ViewModel.ErrorMessage = "Contacting Server...";
            UserModel um = new Model.UserModel(ViewModel.Username, ViewModel.Email);
            byte[] user_login_info = JsonSerializer.SerializeToUtf8Bytes<UserModel>(um);
            HttpResponseMessage response = null;
            HttpContent user_login = new ByteArrayContent(user_login_info);

            try
            {
                HttpResponseMessage resp = await App.Client._client.PostAsync("/login", user_login);
                resp.EnsureSuccessStatusCode();
                Debug.WriteLine(resp);
                Debug.WriteLine("Logged In!");
                ViewModel.ErrorMessage = "Logged In!";
            }
            catch (Exception ex)
            {
                ViewModel.ErrorMessage = "Invalid Username or Email";
                Debug.WriteLine("Invalid Username or Email");
            }
           
            
        }
    }
}
