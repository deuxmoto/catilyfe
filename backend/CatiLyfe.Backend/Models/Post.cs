namespace CatiLyfe.Backend.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

    using CatiLyfe.Backend.App_Code;

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

            this.Content = PostContentFactory.Build(post.PostContent);
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
        public PostContent Content { get; }
    }
}
