namespace CatiLyfe.Backend.Web.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CatiLyfe.Backend.Web.Core.Code;
    using CatiLyfe.Backend.Web.Models.Admin;
    using CatiLyfe.DataLayer;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The admin post controller.
    /// </summary>
    [Route("admin/post")]
    public class AdminPostController : Controller
    {
        /// <summary>
        /// The cati datalayer.
        /// </summary>
        private readonly ICatiDataLayer catiDatalayer;

        /// <summary>
        /// The content transformer.
        /// </summary>
        private readonly IContentTransformer contentTransformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminPostController"/> class.
        /// </summary>
        /// <param name="catiDataLayer">The cati data layer.</param>
        /// <param name="contentTransformer">The content transformer.</param>
        public AdminPostController(ICatiDataLayer catiDataLayer, IContentTransformer contentTransformer)
        {
            this.catiDatalayer = catiDataLayer;
            this.contentTransformer = contentTransformer;
        }

        [HttpGet]
        public async Task<IEnumerable<AdminMetaData>> GetMetadata(int? top, int? skip, DateTime? startDate, DateTime? endDate)
        {
            var metas = await this.catiDatalayer.GetPostMetadata(
                top,
                skip,
                startDate,
                endDate,
                Enumerable.Empty<string>(),
                true);

            return metas.Select(m => new AdminMetaData(m));
        }

        [HttpGet("{id}")]
        public async Task<AdminPost> GetPost(int id)
        {
            var post = await this.catiDatalayer.GetPost(id, true);
            return new AdminPost(post.MetaData, this.contentTransformer.TransformMarkdown(post.PostContent.First().Content));
        }

        /// <summary>
        /// Update or create a post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>The updated post.</returns>
        [HttpPost]
        public async Task<AdminPost> SetPost([FromBody] AdminPost post)
        {
            var updated = await this.catiDatalayer.SetPost(post.ToPost());
            return new AdminPost(updated.MetaData, updated.PostContent.First().Content);
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        /// <param name="id">The post id.</param>
        /// <returns>Nothing or an error.</returns>
        [HttpDelete]
        public Task DeletePost(int id)
        {
            return this.catiDatalayer.DeletePost(id);
        }
    }
}
