using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatiLyfe.Backend.Models;

namespace CatiLyfe.Backend.Controllers
{
    using CatiLyfe.Backend.App_Code;

    [Produces("application/json")]
    [Route("post")]
    public class PostController : Controller
    {
        [HttpGet]
        public async Task<IEnumerable<Post>> GetPosts()
        {
            throw new NotImplementedException();
        }

    }
}