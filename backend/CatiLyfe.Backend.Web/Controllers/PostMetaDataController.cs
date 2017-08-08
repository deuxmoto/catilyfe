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
    /// Retrieves metadata about a post.
    /// </summary>
    [RoutePrefix("api/postmetadata")]
    public class PostMetaDataController : ApiController
    {
        /// <summary>
        /// Gets post metadata based on the search query.
        /// </summary>
        /// <param name="tag">The tag to search.</param>
        /// <param name="top">The number of posts.</param>
        /// <param name="skip">The number to skip.</param>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <returns>The list of post metadata.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IEnumerable<PostMetaData>> GetPostMeta(IEnumerable<string> tag, int ? top = 20, int? skip = 0, DateTime? startdate = null, DateTime? enddate = null)
        {
            var metas = await CatiDataLayer.Datalayer.GetPostMetadata(top: top, skip: skip, startdate: startdate, enddate: enddate, tags: tag);

            return metas.Select(m => new PostMetaData(m));
        }
    }
}
