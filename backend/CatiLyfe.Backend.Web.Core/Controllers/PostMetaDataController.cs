namespace CatiLyfe.Backend.Web.Core.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CatiLyfe.Backend.Web.Models;
    using CatiLyfe.DataLayer;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The post meta data controller.
    /// </summary>
    [Route("/[controller]")]
    public class PostMetaDataController : Controller
    {
        /// <summary>
        /// The data layer implementation.
        /// </summary>
        private readonly ICatiDataLayer datalayer;

        /// <summary>
        /// Initializes the post metadata controller.
        /// </summary>
        /// <param name="datalayer">The data layer.</param>
        public PostMetaDataController(ICatiDataLayer datalayer)
        {
            this.datalayer = datalayer;
        }

        /// <summary>
        /// Get many post meta data objects.
        /// </summary>
        /// <param name="top">The maximum number of posts to return.</param>
        /// <param name="skip">Skip for paging.</param>
        /// <param name="startDate">The start date we are searching for.</param>
        /// <param name="endDate">The end date we are searching for.</param>
        /// <param name="tags">Tags to search by.</param>
        /// <returns>The posts if any.</returns>
        [HttpGet]
        public async Task<IEnumerable<PostMetaModel>> GetMany(
            int? top,
            int? skip,
            DateTime? startDate,
            DateTime? endDate,
            IEnumerable<string> tags)
        {
            var metas = await this.datalayer.GetPostMetadata(
                            top: top,
                            skip: skip,
                            startdate: startDate,
                            enddate: endDate,
                            includeUnpublished: false,
                            includeDeleted: false,
                            tags: tags ?? Enumerable.Empty<string>());
            return metas.Where(m => false == m.IsReserved).Select(m => new PostMetaModel(m));
        }
    }
}
