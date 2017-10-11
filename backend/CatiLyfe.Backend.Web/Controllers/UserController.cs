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
    using System.Configuration;
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
    }
}
