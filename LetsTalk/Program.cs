using LetsTalk.Services;
using DataHandler;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using LetsTalk.Models;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Security.Claims;
using DataHandler.Services;

namespace LetsTalk
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSingleton<IUserDataHandler,Json_UserDataHandler>();
            builder.Services.AddSingleton<IMessageHandler,Json_MessageDataHandler>();
            builder.Services.AddSingleton<IUserService,UserService>();
            builder.Services.AddSingleton<IChatService,ChatService>();
            builder.Services.AddSingleton<IAuthService, JwtAuthService>();
            // builder.Services.AddSingleton(DataHandler.Module.GetRegistration());

            #region JWT Configurations
            var jwtConfig = builder.Configuration.GetSection("JWT_config").Get<JWTConfig>();
            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options => 
            {
                var Key = Encoding.UTF8.GetBytes(jwtConfig.Key);
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtConfig.Issuer,
                    ValidAudience = jwtConfig.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Key),
                };
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireClaim(ClaimTypes.Role, "admin"));
            });

            #endregion

            builder.Services.AddControllers();

 

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

          
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
            Console.WriteLine(DateTime.Now);
        }
    }
}