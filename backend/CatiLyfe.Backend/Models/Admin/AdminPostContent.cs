namespace CatiLyfe.Backend.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The admin post content.
    /// </summary>
    public class AdminPostContent
    {
        public AdminPostContent()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminPostContent"/> class.
        /// </summary>
        /// <param name="markdownContent">
        /// The markdown Content.
        /// </param>
        public AdminPostContent(string markdownContent)
        {
            this.MarkdownContent = markdownContent;
        }

        /// <summary>
        /// Gets the markdown Content.
        /// </summary>
        [Required]
        public string MarkdownContent { get; set; }
    }
}
