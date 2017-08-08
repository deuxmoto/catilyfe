namespace CatiLyfe.Backend.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using CatiLyfe.Backend.Web.App_Start;
    using CatiLyfe.Backend.Web.Models;

    /// <summary>
    /// The post controller.
    /// </summary>
    public class PostController : ApiController
    {
        [HttpGet]
        public async Task<IEnumerable<Post>> GetPosts(int? top, int? skip, DateTime? startdate, DateTime? enddate, IEnumerable<string> tag)
        {
            var posts = await CatiDataLayer.Datalayer.GetPost(top: top, skip: skip, startdate: startdate, enddate: enddate, tags: tag);

            return posts.Select(p => new Post(p));
        }

        [HttpGet]
        public async Task<Post> GetPost(string slug)
        {
            DataLayer.Models.Post post;
            if (int.TryParse(slug, out int postId))
            {
                post = await CatiDataLayer.Datalayer.GetPost(postId);
            }
            else
            {
                post = await CatiDataLayer.Datalayer.GetPost(slug);
            }

            return new Post(post);
        }

        [HttpDelete]
        public async Task DeletePost(string slug)
        {
            await CatiDataLayer.Datalayer.DeletePost(slug);
        }
    }
}
