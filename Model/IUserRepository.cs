using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveMyRPGClient.Model
{
    public interface IUserRepository
    {
        bool AuthenticateUser(UserModel userModel);

        void Register(UserModel userModel);

        void Remove(UserModel userModel);

    }
}
