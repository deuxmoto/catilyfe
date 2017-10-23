namespace CatiLyfe.Backend.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Web.Http;

    using CatiLyfe.Backend.Web.App_Start;
    using CatiLyfe.Backend.Web.Code;
    using CatiLyfe.Backend.Web.Models.Admin;
    using CatiLyfe.Common.Exceptions;

    /// <summary>
    /// Controller for admin operations.
    /// </summary>
    [RoutePrefix("admin")]
    public class AdminController : ApiController
    {
        /// <summary>
        /// Gets metadata for posts.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <returns>The metadata for the post.</returns>
        [HttpGet]
        [Route("post")]
        public async Task<IEnumerable<AdminMetaData>> GetMetadata(
            int? top = 20,
            int? skip = 0,
            DateTime? startdate = null,
            DateTime? enddate = null)
        {
            var metas = await CatiDataLayer.Datalayer.GetPostMetadata(
                            top: top,
                            skip: skip,
                            startdate: startdate,
                            enddate: enddate,
                            isAdmin: true,
                            tags: Enumerable.Empty<string>());

            return
                metas.Select(
                    m => new AdminMetaData(m.Id, m.Title, m.GoesLive, m.WhenCreated, m.Slug, m.Description, m.Tags));
        }

        /// <summary>
        /// Gets a single post.
        /// </summary>
        /// <param name="id">The id of the post.</param>
        /// <returns>The post.</returns>
        [HttpGet]
        [Route("post/{id}")]
        public async Task<AdminPost> GetPost(int id)
        {
            var admin = await CatiDataLayer.Datalayer.GetPost(id, isAdmin: true);
            return
                new AdminPost(
                    new AdminMetaData(
                        admin.MetaData.Id,
                        admin.MetaData.Title,
                        admin.MetaData.GoesLive,
                        admin.MetaData.WhenCreated,
                        admin.MetaData.Slug,
                        admin.MetaData.Description,
                        admin.MetaData.Tags),
                    new AdminPostContent(admin.PostContent.First().Content));
        }

        /// <summary>
        /// Sets a post.
        /// </summary>
        /// <param name="post">The post.</param>
        /// <returns>The result post.</returns>
        [HttpPost]
        [Route("post")]
        public async Task<AdminPost> SetPost([FromBody] AdminPost post)
        {
            var content = new DataLayer.Models.PostContent(0, 0, "markdown", post.MarkdownContent);
            var metaData = new DataLayer.Models.PostMeta(
                post.Metadata.Id ?? -1,
                post.Metadata.Slug,
                post.Metadata.Title,
                post.Metadata.Description,
                post.Metadata.WhenCreated,
                post.Metadata.WhenPublished,
                post.Metadata.Tags);
            var fullPost = new DataLayer.Models.Post(metaData, new[] { content });

            var admin = await CatiDataLayer.Datalayer.SetPost(fullPost);
            return
                new AdminPost(
                    new AdminMetaData(
                        admin.MetaData.Id,
                        admin.MetaData.Title,
                        admin.MetaData.GoesLive,
                        admin.MetaData.WhenCreated,
                        admin.MetaData.Slug,
                        admin.MetaData.Description,
                        admin.MetaData.Tags),
                    new AdminPostContent(admin.PostContent.First().Content));
        }

        /// <summary>
        /// Delete a post.
        /// </summary>
        /// <param name="id">The id of the post.</param>
        /// <returns>No content</returns>
        [HttpDelete]
        [Route("post/{id}")]
        public async Task DeletePost(int id)
        {
            await CatiDataLayer.Datalayer.DeletePost(id);
        }

        /// <summary>
        /// Preview markdown.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The preview.</returns>
        [HttpPost]
        [Route("previewmarkdown")]
        public MarkdownPreview Preview(MarkdownPreviewArgs args)
        {
            try
            {
                return new MarkdownPreview(PostContentFactory.ResolveMarkdown(args.MarkDOWN));
            }
            catch (Exception ex)
            {
                throw new InvalidArgumentException(ex.Message);
            }
        }
    }
}
