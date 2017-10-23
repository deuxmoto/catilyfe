using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace CatiLyfe.Backend.Web.Controllers
{
    using System.Configuration;
    using System.Threading.Tasks;

    using CatiLyfe.Backend.Web.App_Start;
    using CatiLyfe.Backend.Web.Models.Login;
    using CatiLyfe.Common.Security;

    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        private static readonly string PasswordSalt = ConfigurationManager.AppSettings["PasswordSalt"];

        [HttpPost]
        public async Task<LoginDetails> Login(LoginCredentials credentials)
        {
            var user = (await CatiDataLayer.AuthDataLayer.GetUser(null, credentials.Email, null)).First();
            var hashedPassword = PasswordGenerator.HashPassword(LoginController.PasswordSalt, credentials.Password);

            if (false == Convert.ToBase64String(hashedPassword).Equals(Convert.ToBase64String(user.Password)))
            {
                throw new Exception("Could not log in.");
            }

            var token = PasswordGenerator.GenerateTokenBytes(64);
            await CatiDataLayer.AuthDataLayer.CreateToken(user.Id, token, DateTime.UtcNow + TimeSpan.FromDays(7));

            return new LoginDetails { Token = Convert.ToBase64String(token) };
        }
    }
}
