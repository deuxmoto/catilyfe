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
    [RoutePrefix("api/post")]
    public class PostController : ApiController
    {
        /// <summary>
        /// Gets a list of posts.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="startdate">The startdate.</param>
        /// <param name="enddate">The enddate.</param>
        /// <param name="tag">The tag.</param>
        /// <returns>The result set of tasks.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<Post>> GetPosts(IEnumerable<string> tag, int? top = 10, int? skip = 0, DateTime? startdate = null, DateTime? enddate = null)
        {
            var posts = await CatiDataLayer.Datalayer.GetPost(top: top, skip: skip, startdate: startdate, enddate: enddate, tags: tag);

            return posts.Select(p => new Post(p));
        }

        /// <summary>
        /// Get a single post.
        /// </summary>
        /// <param name="slug">The slug of the post.</param>
        /// <returns>The post.</returns>
        [HttpGet]
        [Route("{slug}")]
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

        /// <summary>
        /// Delete a post.
        /// </summary>
        /// <param name="slug">The slug of the post.</param>
        /// <returns>No content</returns>
        [HttpDelete]
        [Route("{slug}")]
        public async Task DeletePost(string slug)
        {
            await CatiDataLayer.Datalayer.DeletePost(slug);
        }
    }
}
