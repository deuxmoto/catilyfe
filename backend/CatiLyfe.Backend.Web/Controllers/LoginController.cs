namespace CatiLyfe.Backend.Web.Controllers
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using CatiLyfe.Backend.Web.App_Start;
    using CatiLyfe.Backend.Web.Models.Login;
    using CatiLyfe.Common.Security;

    /// <summary>
    /// A controller to login bros.
    /// </summary>
    [RoutePrefix("login")]
    public class LoginController : ApiController
    {
        /// <summary>
        /// The password salt.
        /// </summary>
        private static readonly string PasswordSalt = ConfigurationManager.AppSettings["PasswordSalt"];

        /// <summary>
        /// Login one of the bros.
        /// </summary>
        /// <param name="credentials">The credentials for that.</param>
        /// <returns>The token to be using.</returns>
        [HttpPost]
        [Route("")]
        public async Task<LoginDetails> Login(LoginCredentials credentials)
        {
            var user = (await CatiDataLayer.AuthDataLayer.GetUser(null, credentials.Email, null)).First();
            var hashedPassword = PasswordGenerator.HashPassword(LoginController.PasswordSalt, credentials.Password);

            PasswordValidator.Validate(user.Password, hashedPassword);

            var token = PasswordGenerator.GenerateTokenBytes(64);
            await CatiDataLayer.AuthDataLayer.CreateToken((int)user.Id, token, DateTime.UtcNow + TimeSpan.FromDays(7));

            return new LoginDetails { Token = Convert.ToBase64String(token) };
        }
    }
}
