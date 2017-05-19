namespace CatiLyfe.Backend.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class RootController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}