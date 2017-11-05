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
    using CatiLyfe.Backend.Web.Code.Filters;
    using CatiLyfe.Backend.Web.Models.User;
    using CatiLyfe.Common.Security;
    using CatiLyfe.DataLayer.Models;

    [RoutePrefix("user")]
    [Authenticate]
    public class UserController : ApiController
    {
        /// <summary>
        /// The password salt.
        /// </summary>
        private static readonly string PasswordSalt = ConfigurationManager.AppSettings["PasswordSalt"];

        /// <summary>
        /// Create or update a user. Empty id to create a new user.
        /// </summary>
        /// <param name="user">The user.</param>
        /// <returns>Nothing or an error.</returns>
        [HttpPut]
        [Route("")]
        public Task SetUser(User user)
        {
            // Null to not change this.
            var password = user.Password != null
                               ? PasswordGenerator.HashPassword(PasswordSalt, user.Password)
                               : null;

            var userModel = new UserModel(
                user.Id,
                user.Name,
                user.Email,
                password,
                new HashSet<string>(user.Roles));

            return CatiDataLayer.AuthDataLayer.SetUser(userModel);
        }

        /// <summary>
        /// Get all of the users.
        /// </summary>
        /// <returns>All users and their roles.</returns>
        [HttpGet]
        [Route("all")]
        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await CatiDataLayer.AuthDataLayer.GetUser(null, null, null);
            return users.Select(
                u => new User() { Id = u.Id, Email = u.Email, Name = u.Name, Roles = u.Roles.ToArray() });
        }

        /// <summary>
        /// Gets all configured roles in the database.
        /// </summary>
        /// <returns>The roles.</returns>
        [HttpGet]
        [Route("role")]
        public async Task<IEnumerable<UserRole>> GetRoles()
        {
            var roles = await CatiDataLayer.AuthDataLayer.GetRoles();
            return roles.Select(r => new UserRole(r));
        }

        /// <summary>
        /// Delete a role.
        /// </summary>
        /// <param name="name">The name of the role.</param>
        /// <returns>Nothing or error.</returns>
        [HttpDelete]
        [Route("role/{name}")]
        public Task DeleteRole(string name)
        {
            return CatiDataLayer.AuthDataLayer.DeleteRole(name: name, commit: false);
        }

        /// <summary>
        /// Create or alter the description of a role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>Nothing or error.</returns>
        [HttpPut]
        [Route("role")]
        public Task SetRole(UserRole role)
        {
            return CatiDataLayer.AuthDataLayer.SetRole(new UserRoleDescription(role.Role, role.Description));
        }
    }
}
