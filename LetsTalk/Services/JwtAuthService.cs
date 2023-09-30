using DataHandler;
using LetsTalk.Models;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace LetsTalk.Services
{
    public class JwtAuthService : IAuthService
    {
        private IConfiguration _config;
        public JwtAuthService(IConfiguration config)
        {
            _config = config;

        }

        public Task<string> GenerateSessionToken(UserBase userDetails)
        {
            var _jwtConfig = _config.GetSection("JWT_config").Get<JWTConfig>();
            if (_jwtConfig == null)
            {
                throw new Exception("unable to read jwt configuration");
            }
            string key = _jwtConfig.Key;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, userDetails.UserName),
                new Claim(ClaimTypes.Role, userDetails.Role.ToString()),
                new Claim(ClaimTypes.Email, userDetails.Email),
                new Claim(ClaimTypes.GivenName, userDetails.GivenName),
            };

            var token = new JwtSecurityToken(
                _jwtConfig.Issuer, _jwtConfig.Audience, claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials
                );

            var jwt =  new JwtSecurityTokenHandler().WriteToken(token);
            return Task.FromResult(jwt);
        }
       
    }
    
}