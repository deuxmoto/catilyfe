namespace CatiLyfe.Backend.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The post content.
    /// </summary>
    public class PostContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostContent"/> class.
        /// </summary>
        /// <param name="content">The content.</param>
        internal PostContent(string content)
        {
            this.Content = content;
        }

        /// <summary>
        /// The full resolved body of the post.
        /// </summary>
        [Required]
        public string Content { get; }
    }
}
