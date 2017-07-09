using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using CatiLyfe.Backend.App_Code;

namespace CatiLyfe.Backend.Controllers
{
    using CatiLyfe.Backend.Models;
    using CatiLyfe.Backend.Models.Admin;
    using CatiLyfe.DataLayer.Models;

    [Produces("application/json")]
    [Route("api/Admin")]
    public class AdminController : Controller
    {
        /// <summary>
        /// Gets metadata for posts.
        /// </summary>
        /// <param name="top">The top.</param>
        /// <param name="skip">The skip.</param>
        /// <param name="startdate">The start date.</param>
        /// <param name="enddate">The end date.</param>
        /// <returns></returns>
        [HttpGet("meta")]
        public async Task<IEnumerable<AdminMetaData>> GetMetadata([FromQuery] int? top, [FromQuery] int? skip, [FromQuery] DateTime? startdate, [FromQuery] DateTime? enddate)
        {
            var metas = await CatiData.Datalayer.GetPostMetadata(top: top, skip: skip, startdate: startdate, enddate: enddate, tags: Enumerable.Empty<string>());

            return metas.Select(m => new AdminMetaData(m.Id, m.Title, m.GoesLive, m.WhenCreated, m.Slug, m.Description, m.Tags));
        }

        [HttpGet("edit/{slug}")]
        public async Task<AdminPost> GetPost(string slug)
        {
            var admin = await CatiData.Datalayer.GetPost(slug);
            return new AdminPost(new AdminMetaData(admin.MetaData.Id, admin.MetaData.Title, admin.MetaData.GoesLive, admin.MetaData.WhenCreated, admin.MetaData.Slug, admin.MetaData.Description, admin.MetaData.Tags), new AdminPostContent(admin.PostContent.First().Content));
        }

        [HttpPost("edit")]
        public async Task<AdminPost> SetPost([FromBody]AdminPost post)
        {
            var content = new DataLayer.Models.PostContent(0, 0, "markdown", post.PostContent.MarkdownContent);
            var metaData = new DataLayer.Models.PostMeta(post.MetaData.Id ?? -1, post.MetaData.Slug, post.MetaData.Title, post.MetaData.Description, post.MetaData.WhenCreated, post.MetaData.LiveTime, post.MetaData.Tags);
            var fullPost = new DataLayer.Models.Post(metaData, new [] {content});

            var admin =  await CatiData.Datalayer.SetPost(fullPost);
            return new AdminPost(new AdminMetaData(admin.MetaData.Id, admin.MetaData.Title, admin.MetaData.GoesLive, admin.MetaData.WhenCreated, admin.MetaData.Slug, admin.MetaData.Description, admin.MetaData.Tags), new AdminPostContent(admin.PostContent.First().Content));
        }
    }
}