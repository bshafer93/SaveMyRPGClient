using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Model
{
    public class UserModel
    {
        public string Password { get; set; }
        public string Email { get; set; }


        public UserModel(string password, string email) { 
            this.Password = password;
            Email = email;
        }
    }
}
