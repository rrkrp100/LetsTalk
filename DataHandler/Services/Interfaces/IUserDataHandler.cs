using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataHandler
{
    public interface IUserDataHandler
    {
        public Task<bool> DoesUserAlreadyExist(string username);
        public Task<UserData> GetUserIfPresent(string username);
        public Task<UserData> CreateUser(UserData username);
    }
}
