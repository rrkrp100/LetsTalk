using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LetsTalk.Services;
using LetsTalk.Models;

namespace LetsTalk.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : Controller
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("signin")]
        public async Task<ActionResult<UserViewModel>> UserSignIn([FromBody] UserSigninDetails userSignIn)
        {
            if(! ValidationService.AreUserDetailsValid(userSignIn.UserName, userSignIn.Password, out UserViewModel validationResponse))
              return new BadRequestObjectResult(validationResponse);


            var response = await _userService.ProcessUserLoginRequest(userSignIn.UserName, userSignIn.Password);
            if (response != null && response.Errors==null)
            {
                return new OkObjectResult(response);
            }
            return new BadRequestObjectResult(response);
        }

        [HttpPost("signup")]
        public async Task<ActionResult<UserViewModel>> UserSignup([FromBody] UserBase user)
        {
            if (!ValidationService.AreUserDetailsValid(user, out UserViewModel validationResponse))
                return new BadRequestObjectResult(validationResponse);

            UserViewModel? response = await _userService.ProcessUserRegistrationRequest(user);
            if (response != null && response.Errors == null)
            {
                return new OkObjectResult(response);
            }
            return new BadRequestObjectResult(response);
        }

        [Authorize(Policy = "AdminPolicy")]
        [HttpGet("{id}")]
        public async Task<ActionResult<string>> GetUser(string id)
        {
            return Task.FromResult("gotIt").Result;
        }
    }
}
