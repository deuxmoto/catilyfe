using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatiLyfe.Backend.Controllers
{
    using CatiLyfe.Backend.Models;

    [Produces("application/json")]
    [Route("status")]
    public class StatusController : Controller
    {
        [HttpGet]
        public StatusModel GetTest()
        {
            return new StatusModel(DateTime.UtcNow, "Service is up.");
        }
    }
}