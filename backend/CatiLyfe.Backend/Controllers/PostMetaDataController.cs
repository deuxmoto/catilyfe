using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CatiLyfe.Backend.Controllers
{
    using CatiLyfe.Backend.App_Code;
    using CatiLyfe.Backend.Models;

    [Produces("application/json")]
    [Route("postmetadata")]
    public class PostMetaDataController : Controller
    {
        [HttpGet()]
        public async Task<IEnumerable<PostMetaData>> GetPostMeta([FromQuery] int? top, [FromQuery] int? skip, [FromQuery] DateTime? startdate, [FromQuery] DateTime? enddate)
        {
            var metas = await CatiData.Datalayer.GetPostMetadata(top: top, skip: skip, startdate: startdate, enddate: enddate);

            return metas.Select(m => new PostMetaData(m));
        }
    }
}