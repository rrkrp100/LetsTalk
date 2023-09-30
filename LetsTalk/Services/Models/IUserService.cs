using DataHandler;
using LetsTalk.Models;

namespace LetsTalk.Services
{
    public interface IUserService
    {
        public Task<UserViewModel> ProcessUserLoginRequest(string userName, string password);
        public Task<UserViewModel> ProcessUserRegistrationRequest(UserBase userDetails);
    }
}
