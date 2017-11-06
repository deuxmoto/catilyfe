namespace CatiLyfe.Backend.Web.Core.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CatiLyfe.Backend.Web.Models.User;
    using CatiLyfe.Common.Security;
    using CatiLyfe.DataLayer;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The userModel controller.
    /// </summary>
    [Route("[controller]")]
    public class UserController : Controller
    {
        /// <summary>
        /// The data layer implementation.
        /// </summary>
        private readonly ICatiAuthDataLayer authDataLayer;

        /// <summary>
        /// The password helper.
        /// </summary>
        private readonly IPasswordHelper passwordHelper;

        /// <summary>
        /// Initializes the post metadata controller.
        /// </summary>
        /// <param name="authDataLayer">The auth data layer.</param>
        public UserController(ICatiAuthDataLayer authDataLayer, IPasswordHelper passwordHelper)
        {
            this.authDataLayer = authDataLayer;
            this.passwordHelper = passwordHelper;
        }

        /// <summary>
        /// Create or update a userModel.
        /// </summary>
        /// <param name="userModel">The userModel.</param>
        /// <returns>Nothing, or an error on failure.</returns>
        [HttpPut]
        public Task SetUser([FromBody]UserModel userModel)
        {
            return this.authDataLayer.SetUser(userModel.ToUser(this.passwordHelper));
        }

        /// <summary>
        /// Get all users.
        /// </summary>
        /// <returns>The users.</returns>
        [HttpGet]
        public async Task<IEnumerable<UserModel>> GetAllUsers()
        {
            var users = await this.authDataLayer.GetUser(null, null, null);
            return users.Select(u => new UserModel(u));
        }
    }
}
