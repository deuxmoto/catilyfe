namespace CatiLyfe.Backend.Web.Core.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CatiLyfe.DataLayer;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The auth test controller.
    /// </summary>
    [Route("[controller]")]
    [Authorize(Policy = "default")]
    public class AuthTestController : Controller
    {
        /// <summary>
        /// The auth data layer.
        /// </summary>
        private readonly ICatiAuthDataLayer authDataLayer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AuthTestController"/> class.
        /// </summary>
        /// <param name="authDatalayer">The auth datalayer.</param>
        public AuthTestController(ICatiAuthDataLayer authDatalayer)
        {
            this.authDataLayer = authDatalayer;
        }

        /// <summary>
        /// Test the authorization.
        /// </summary>
        /// <returns>The authorization.</returns>
        [HttpGet]
        public Task<IEnumerable<string>> GetClaims()
        {
            var claims = this.HttpContext.User.Claims;
            return Task.FromResult(claims.Select(c => $"{c.Type} -> {c.Value}"));
        }
    }
}

