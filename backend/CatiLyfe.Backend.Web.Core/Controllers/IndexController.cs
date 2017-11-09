namespace CatiLyfe.Backend.Web.Core.Controllers
{
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The index controller.
    /// </summary>
    [Route("")]
    public class IndexController : Controller
    {
        /// <summary>
        /// The home page.
        /// </summary>
        /// <returns>The view.</returns>
        [HttpGet]
        public ViewResult HomePage()
        {
            return this.View();
        }

        /// <summary>
        /// Redirect to swagger.
        /// </summary>
        /// <returns>The redirect.</returns>
        [HttpGet("api")]
        public RedirectResult Api()
        {
            return this.Redirect("~/swagger");
        }
    }
}
