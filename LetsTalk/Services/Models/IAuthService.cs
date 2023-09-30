using DataHandler;
using LetsTalk.Models;

namespace LetsTalk.Services
{
    public interface IAuthService
    {
        public Task<string> GenerateSessionToken(UserBase userDetails);
    }
}
