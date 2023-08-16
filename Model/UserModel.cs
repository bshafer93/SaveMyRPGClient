using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Model
{
    public class UserModel
    {
        public string Username { get; set; }
        public string Email { get; set; }


        public UserModel(string username, string email) { 
            Username = username;
            Email = email;
        }
    }
}
