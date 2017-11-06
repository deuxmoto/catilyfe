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
        /// <param name="post">The post.</param>
        public PostModel(Post post)
        {
            this.Metadata = new PostMetaModel(post.MetaData);
            this.RawHtmlThenIGuess = ""; //PostContentFactory.Build(post.PostContent).RawHtmlThenIGuess;
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
