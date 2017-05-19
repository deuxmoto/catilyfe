namespace CatiLyfe.Backend.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    [Route("")]
    public class RootController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}