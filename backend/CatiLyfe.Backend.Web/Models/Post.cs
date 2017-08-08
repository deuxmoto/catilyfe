namespace CatiLyfe.Backend.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The post.
    /// </summary>
    public class Post
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Post"/> class.
        /// </summary>
        /// <param name="post">The post.</param>
        public Post(DataLayer.Models.Post post)
        {
            this.Metadata = new PostMetaData(post.MetaData);

            this.RawHtmlThenIGuess = PostContentFactory.Build(post.PostContent).RawHtmlThenIGuess;
        }

        /// <summary>
        /// The metadata for the post.
        /// </summary>
        [Required]
        public PostMetaData Metadata { get; }

        /// <summary>
        /// The content for the post.
        /// </summary>
        [Required]
        public string RawHtmlThenIGuess { get; }
    }
}
