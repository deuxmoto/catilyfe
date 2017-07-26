namespace CatiLyfe.Backend.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using CatiLyfe.Backend.App_Code;
    using CatiLyfe.Backend.Models;

    /// <summary>
    /// The controller for tag related actions.
    /// </summary>
    [Produces("application/json")]
    [Route("api/Tag")]
    public class TagController : Controller
    {
        /// <summary>
        /// Get a list of all tags, along with their popularaity.
        /// </summary>
        /// <returns></returns>
        [HttpGet("")]
        public async Task<IEnumerable<TagInfo>> GetTags()
        {
            var tags = await CatiData.Datalayer.GetTags();
            return tags.Select(pt => new TagInfo(pt.Tag, pt.Weight));
        }
    }
}