using DataHandler;
using LetsTalk.Models;

namespace LetsTalk.Services
{
    public static class ValidationService
    {
        public static bool AreUserDetailsValid(UserBase user, out UserViewModel response)
        {
            response = new UserViewModel()
            {
                Errors = new List<Error>()
            };
            if (!IsValidUserName(user.UserName)) response.Errors.Add(new Error() { Message = "Invalid User", Code = 401 });
            if (!IsValidPassword(user.Password)) response.Errors.Add(new Error() { Message = "Invalid Password", Code = 401 });
            if (!IsValidEmail(user.Email)) response.Errors.Add(new Error() { Message = "Invalid Email", Code = 401 }); ;

            if (response.Errors.Count > 0)
                return false;

            return true;
        }

        public static bool AreUserDetailsValid(string username,string password, out UserViewModel response)
        {
            response = new UserViewModel()
            {
                Errors = new List<Error>()
            };
            if (!IsValidUserName(username)) response.Errors.Add(new Error() { Message = "Invalid User", Code = 401 });
            if (!IsValidPassword(password)) response.Errors.Add(new Error() { Message = "Invalid Password", Code = 401 });

            if (response.Errors.Count > 0) 
                return false;
            
            return true;
        }


        public static bool IsValidUserName(string username)
        {
            return true;
        }
        public static bool IsValidPassword(string password)
        {
            return true;
        }
        public static bool IsValidEmail(string email)
        {
            return true;
        }
    }
}
