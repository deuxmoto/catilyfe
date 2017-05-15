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
        public IEnumerable<PostMetaData> GetPosts()
        {
            var temp1 = new PostMetaData
                            {
                                Id = 0,
                                Title = "Cati test post 1 yo",
                                WhenPublished = new DateTime(2017, 5, 13),
                                Slug = "cati-test-post-1-yo"
                            };

            var temp2 = new PostMetaData
            {
                Id = 2,
                Title = "Cati test post TWO yo",
                WhenPublished = new DateTime(2017, 5, 13),
                Slug = "cati-test-post-two-yo"
            };

            return new[] { temp1, temp2 };
        }
    }
}