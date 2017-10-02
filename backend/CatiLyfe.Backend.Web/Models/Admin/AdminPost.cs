namespace CatiLyfe.Backend.Web.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The admin post.
    /// </summary>
    public class AdminPost
    {
        public AdminPost()
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AdminPost"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="postContent">The content.</param>
        public AdminPost(AdminMetaData metadata, AdminPostContent postContent)
        {
            this.Metadata = metadata;
            this.MarkdownContent = postContent.MarkdownContent;
        }

        /// <summary>
        /// The metadata.
        /// </summary>
        [Required]
        public AdminMetaData Metadata { get; set; }

        /// <summary>
        /// Gets the post content.
        /// </summary>
        [Required]
        public string MarkdownContent { get; set; }
    }
}
