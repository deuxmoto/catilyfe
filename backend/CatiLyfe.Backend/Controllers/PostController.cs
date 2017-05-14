using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatiLyfe.Backend.Models;

namespace CatiLyfe.Backend.Controllers
{
    [Produces("application/json")]
    [Route("post")]
    public class PostController : Controller
    {
        [HttpGet]
        public IEnumerable<Post> GetPosts()
        {
            var temp1 = new Post
                            {
                                Title = "Cati test post 1 yo",
                                LiveTime = new DateTime(2017, 5, 13),
                                Content = "This is the first test post yo.",
                                Slug = "cati-test-post-1-yo"
                            };

            var temp2 = new Post
            {
                Title = "Cati test post TWO yo",
                LiveTime = new DateTime(2017, 5, 13),
                Content = "This is the SECOND YO test post yo.",
                Slug = "cati-test-post-two-yo"
            };

            return new[] { temp1, temp2 };
        }
    }
}