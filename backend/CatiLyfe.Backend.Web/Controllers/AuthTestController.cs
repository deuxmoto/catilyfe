namespace CatiLyfe.Backend.Web.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Http;

    using CatiLyfe.Backend.Web.Code.Filters;
    using CatiLyfe.Backend.Web.Models.AuthTest;

    /// <summary>
    /// The authentication test controller.
    /// </summary>
    [Authenticate]
    [RoutePrefix("authtest")]
    public class AuthTestController : ApiController
    {
        /// <summary>
        /// The test method.
        /// </summary>
        /// <returns>A friendly message on success.</returns>
        [HttpGet]
        [Route("")]
        public AuthTestResult Test()
        {
            var user = HttpContext.Current.User.Identity;
            return new AuthTestResult("CATI AUTH SUCCESS YO.", user.Name, Enumerable.Empty<string>());
        }
    }
}
