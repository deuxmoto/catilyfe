namespace CatiLyfe.Backend.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;

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
            this.Content = post.PostContent.Select(c => new PostContent(c));
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
        public IEnumerable<PostContent> Content { get; }
    }
}
