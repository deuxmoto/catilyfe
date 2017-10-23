namespace CatiLyfe.Backend.Web.Models.Admin
{
    using System.Web.Mvc;

    /// <summary>
    /// The markdown preview result object.
    /// </summary>
    public class MarkdownPreview
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MarkdownPreview"/> object.
        /// </summary>
        /// <param name="content"></param>
        public MarkdownPreview(string content)
        {
            this.Content = content;
        }

        /// <summary>
        /// The html content.
        /// </summary>
        [AllowHtml]
        public string Content { get; }
    }
}