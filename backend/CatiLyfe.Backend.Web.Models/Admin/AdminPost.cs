namespace CatiLyfe.Backend.Web.Models.Admin
{
    using System.ComponentModel.DataAnnotations;

    using CatiLyfe.DataLayer.Models;

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
        public AdminPost(PostMeta metadata, string postContent)
        {
            this.Metadata = new AdminMetaData(metadata);
            this.MarkdownContent = postContent;
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

        /// <summary>
        /// Convert this object to a post.
        /// </summary>
        /// <returns>The post.</returns>
        public Post ToPost()
        {
            return new Post(this.Metadata.ToPostMeta(), new []{ new PostContent(0, 0, "markdown", this.MarkdownContent), });
        }
    }
}
