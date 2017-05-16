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
        public async Task<IEnumerable<Post>> GetPosts([FromQuery] int? top, [FromQuery] int? skip, [FromQuery] DateTime? startdate, [FromQuery] DateTime? enddate)
        {
            var posts = await CatiData.Datalayer.GetPost(top: top, skip: skip, startdate: startdate, enddate: enddate);

            return posts.Select(p => new Post(p));
        }

        [HttpGet("{slug}")]
        public async Task<Post> GetPost(string slug)
        {
            DataLayer.Models.Post post;
            if (int.TryParse(slug, out int postId))
            {
                post = await CatiData.Datalayer.GetPost(postId);
            }
            else
            {
                post = await CatiData.Datalayer.GetPost(slug);
            }

            return new Post(post);
        }
    }
}