namespace CatiLyfe.Backend.Web.Core.Controllers
{
    using System.Threading.Tasks;

    using CatiLyfe.Backend.Web.Core.Code;
    using CatiLyfe.Backend.Web.Models.Admin;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The admin markdown controller.
    /// </summary>
    [Route("admin/previewmarkdown")]
    [Authorize(Policy = "default")]
    public class AdminMarkdownController : Controller
    {
        /// <summary>
        /// The content transformer.
        /// </summary>
        private readonly IContentTransformer contentTransformer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminMarkdownController"/> class.
        /// </summary>
        /// <param name="contentTransformer">The content transformer.</param>
        public AdminMarkdownController(IContentTransformer contentTransformer)
        {
            this.contentTransformer = contentTransformer;
        }

        /// <summary>
        /// Request a rendered set of markdown.
        /// </summary>
        /// <param name="args">The arguments.</param>
        /// <returns>The preview.</returns>
        [HttpPost]
        [Authorize(Policy = "default", Roles = "god-post,post-add,post-edit")]
        public Task<MarkdownPreview> PreviewMarkdown([FromBody]MarkdownPreviewArgs args)
        {
            var result = this.contentTransformer.TransformMarkdown(args.MarkDOWN);
            return Task.FromResult(new MarkdownPreview(result));
        }
    }
}
