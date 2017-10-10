namespace CatiLyfe.Backend.Web.Controllers
{
    using System.Web.Http;

    using CatiLyfe.Backend.Web.Code.Filters;
    using CatiLyfe.Backend.Web.Models.AuthTest;

    /// <summary>
    /// The authentication test controller.
    /// </summary>
    [Authorize]
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
            return new AuthTestResult("CATI AUTH SUCCESS YO.");
        }
    }
}
