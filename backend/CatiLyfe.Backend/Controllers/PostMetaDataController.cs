namespace CatiLyfe.Backend.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using CatiLyfe.Backend.App_Code;
    using CatiLyfe.Backend.Models;

    using Microsoft.AspNetCore.Mvc;

    [Produces("application/json")]
    [Route("postmetadata")]
    public class PostMetaDataController : Controller
    {
        [HttpGet()]
        public async Task<IEnumerable<PostMetaData>> GetPostMeta([FromQuery] int? top, [FromQuery] int? skip, [FromQuery] DateTime? startdate, [FromQuery] DateTime? enddate, [FromQuery]IEnumerable<string> tag)
        {
            var metas = await CatiData.Datalayer.GetPostMetadata(top: top, skip: skip, startdate: startdate, enddate: enddate, tags: tag);

            return metas.Select(m => new PostMetaData(m));
        }
    }
}