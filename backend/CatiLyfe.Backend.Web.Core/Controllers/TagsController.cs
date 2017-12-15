using CatiLyfe.DataLayer;
using Microsoft.AspNetCore.Mvc;

namespace CatiLyfe.Backend.Web.Core.Controllers
{
    public class TagsController : Controller
    {
        private readonly ICatiDataLayer dataLayer;

        public TagsController(ICatiDataLayer datalayer)
        {
            this.dataLayer = datalayer;
        }


    }
}
