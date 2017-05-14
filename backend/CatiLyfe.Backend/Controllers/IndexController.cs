using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CatiLyfe.Backend.Controllers
{
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