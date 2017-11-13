namespace CatiLyfe.Backend.Web.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CatiLyfe.Backend.Web.Core.Code;
    using CatiLyfe.Backend.Web.Models;
    using CatiLyfe.DataLayer;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The post controller.
    /// </summary>
    [Route("/[controller]")]
    public class PostController : Controller
    {
        /// <summary>
        /// The data layer implementation.
        /// </summary>
        private readonly ICatiDataLayer datalayer;

        /// <summary>
        /// The content transformer.
        /// </summary>
        private readonly IContentTransformer contentTransformer;

        /// <summary>
        /// Initializes the post controller.
        /// </summary>
        /// <param name="datalayer">The data layer.</param>
        public PostController(ICatiDataLayer datalayer, IContentTransformer contentTransformer)
        {
            this.datalayer = datalayer;
            this.contentTransformer = contentTransformer;
        }

        /// <summary>
        /// Get many posts.
        /// </summary>
        /// <param name="top">The maximum number of posts to return.</param>
        /// <param name="skip">Skip for paging.</param>
        /// <param name="startDate">The startdate we are searching for.</param>
        /// <param name="endDate">The endate we are searching for.</param>
        /// <param name="tags">Tags to search by.</param>
        /// <returns>The posts if any.</returns>
        [HttpGet]
        public async Task<IEnumerable<PostModel>> GetMany(int? top, int? skip, DateTime? startDate, DateTime? endDate, IEnumerable<string> tags)
        {
            var posts = await this.datalayer.GetPost(
                            top: top,
                            skip: skip,
                            startdate: startDate,
                            enddate: endDate,
                            includeUnpublished: false,
                            includeDeleted: false,
                            tags: tags ?? Enumerable.Empty<string>());
            return posts.Where(predicate: p => false == p.MetaData.IsReserved).Select(
                selector: p => PostModel.Create(
                    metadata: p.MetaData,
                    content: this.contentTransformer.TransformMarkdown(markdown: p.PostContent.First().Content)));
        }

        /// <summary>
        /// Get a single post by its slug.
        /// </summary>
        /// <param name="slug">The slug of the post.</param>
        /// <returns>The post if it exist.</returns>
        [HttpGet("{slug}")]
        public async Task<PostModel> GetSingle(string slug)
        {
            var post = await this.datalayer.GetPost(slug: slug, includeUnpublished: false, includeDeleted: false);
            return PostModel.Create(post.MetaData, this.contentTransformer.TransformMarkdown(post.PostContent.First().Content));
        }
    }
}
