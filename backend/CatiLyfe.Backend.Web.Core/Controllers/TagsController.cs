using CatiLyfe.Backend.Web.Models.Tags;
using CatiLyfe.DataLayer;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatiLyfe.Backend.Web.Core.Controllers
{
    [Route("/[controller]")]
    public class TagsController : Controller
    {
        private readonly ICatiDataLayer dataLayer;

        public TagsController(ICatiDataLayer datalayer)
        {
            this.dataLayer = datalayer;
        }

        [HttpGet]
        public async Task<IEnumerable<TagInfoModel>> GetTags()
        {
            var tags = await this.dataLayer.GetTags();
            return tags.Select(t => new TagInfoModel(t.Tag, t.Weight));
        }
    }
}
