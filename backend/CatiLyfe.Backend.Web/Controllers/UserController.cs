// --------------------------------------------------------------------------------------------------------------------
// <copyright file="UserController.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the UserController type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CatiLyfe.Backend.Web.Controllers
{
    using System.Collections.Generic;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using CatiLyfe.Backend.Web.App_Start;
    using CatiLyfe.Backend.Web.Models.User;
    using CatiLyfe.Common.Security;
    using CatiLyfe.DataLayer.Models;

    [RoutePrefix("user")]
    public class UserController : ApiController
    {
        private static readonly string PasswordSalt = ConfigurationManager.AppSettings["PasswordSalt"];

        [HttpPut]
        [Route("create")]
        public Task CreateUser(CreateUser createUserArgs)
        {
            var password = PasswordGenerator.HashPassword(PasswordSalt, createUserArgs.Password);

            var userModel = new UserModel(-1, createUserArgs.Name, createUserArgs.Email, password);

            return CatiDataLayer.AuthDataLayer.SetUser(userModel);
        }

        /// <summary>
        /// Get all of the users.
        /// </summary>
        /// <returns>All users and their roles.</returns>
        [HttpGet]
        [Route]
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await CatiDataLayer.AuthDataLayer.GetUser(null, null, null);
            return users.Select(
                u => new User() { Id = u.Id, Email = u.Email, Name = u.Name, Roles = u.Roles.ToList() });
        }
    }
}
