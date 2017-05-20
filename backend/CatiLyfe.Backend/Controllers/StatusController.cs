namespace CatiLyfe.Backend.Controllers
{
    using System;

    using CatiLyfe.Backend.Models;

    using Microsoft.AspNetCore.Mvc;

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