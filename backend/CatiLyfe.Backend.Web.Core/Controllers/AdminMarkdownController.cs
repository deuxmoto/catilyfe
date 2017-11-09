namespace CatiLyfe.Backend.Web.Core.Controllers
{
    using System.Threading.Tasks;

    using CatiLyfe.Backend.Web.Core.Code;
    using CatiLyfe.Backend.Web.Models.Admin;

    using Microsoft.AspNetCore.Mvc;

    /// <summary>
    /// The admin markdown controller.
    /// </summary>
    [Route("admin/previewmarkdown")]
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
        public Task<MarkdownPreview> PreviewMarkdown(MarkdownPreviewArgs args)
        {
            var result = this.contentTransformer.TransformMarkdown(args.MarkDOWN);
            return Task.FromResult(new MarkdownPreview(result));
        }
    }
}
