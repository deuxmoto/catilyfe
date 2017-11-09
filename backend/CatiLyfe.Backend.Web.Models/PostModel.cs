namespace CatiLyfe.Backend.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    using CatiLyfe.DataLayer.Models;

    /// <summary>
    /// The post.
    /// </summary>
    public class PostModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostModel"/> class.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="html">The html.</param>
        private PostModel(PostMeta metadata, string html)
        {
            this.Metadata = new PostMetaModel(metadata);
            this.RawHtmlThenIGuess = html;
        }

        /// <summary>
        /// Create a post model.
        /// </summary>
        /// <param name="metadata">The metadata.</param>
        /// <param name="content">The content.</param>
        /// <returns>The <see cref="PostModel"/>.</returns>
        public static PostModel Create(PostMeta metadata, string content)
        {
            return new PostModel(metadata, content);
        }

        /// <summary>
        /// The metadata for the post.
        /// </summary>
        [Required]
        public PostMetaModel Metadata { get; }

        /// <summary>
        /// The content for the post.
        /// </summary>
        [Required]
        public string RawHtmlThenIGuess { get; }
    }
}
