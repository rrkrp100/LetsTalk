using DataHandler;
using LetsTalk.Models;

namespace LetsTalk.Services
{
    public class UserService : IUserService
    {
        private IAuthService _authService;
        private IUserDataHandler _dataHandlerService;
        public UserService(IAuthService authService, IUserDataHandler dataHandlerService)
        {
            _authService = authService;
            _dataHandlerService = dataHandlerService;
        }
        public async Task<UserViewModel> ProcessUserLoginRequest(string userName, string password)
        {
            UserBase userDetails = DataTranslatorService.ToUserBase(await _dataHandlerService.GetUserIfPresent(userName));
            //var dbUser = await _dataHandlerService.GetUserIfPresent(userName);
            //UserBase userDetails = null;

            if (userDetails == null)
            {
                return GetErrorModel("User not Found.!", 404);
            }
            //userDetails = dbUser.ToUserBase();
            if (userDetails.Password == password)
            {
                var user = new UserViewModel()
                {
                    GivenName = userDetails.GivenName,
                    Email = userDetails.Email,
                    UserName = userDetails.UserName,
                    SessionToken = await _authService.GenerateSessionToken(userDetails),
                    Role = userDetails.Role,
                    Age = userDetails.Age,
                };
                return user;
            }
            else
            {
                return GetErrorModel("Wrong Password.!", 401);
            }
        }

        public async Task<UserViewModel> ProcessUserRegistrationRequest(UserBase userDetails)
        {
            bool userAlreadyExists = await _dataHandlerService.DoesUserAlreadyExist(userDetails.UserName);

            if (userAlreadyExists)
            {
                return GetErrorModel("User Already Exists.!", 401);
            }

            try 
            {
                UserBase? savedUserDetails = DataTranslatorService.ToUserBase(await _dataHandlerService.CreateUser(userDetails.ToDbUser()));
                if(savedUserDetails == null)
                {
                    return GetErrorModel("Could not create user.!", 500);
                }
                return new UserViewModel()
                {
                    GivenName = savedUserDetails.GivenName,
                    Email = savedUserDetails.Email,
                    UserName = savedUserDetails.UserName,
                    SessionToken = await _authService.GenerateSessionToken(savedUserDetails),
                    Role = savedUserDetails.Role,
                    Age = savedUserDetails.Age,
                };
            } 
            catch(Exception ex)
            {
                Console.WriteLine(ex);
                return GetErrorModel("Could not create user.!", 500);
            }
            

        }

        private static UserViewModel GetErrorModel(string errorMessage, int errorCode)
        {
            return new UserViewModel()
            {
                Errors = new List<Error>()
                    {
                        new Error()
                        {
                            Message = errorMessage,
                            Code = errorCode
                        }
                    }
            };
        }
    }
}
