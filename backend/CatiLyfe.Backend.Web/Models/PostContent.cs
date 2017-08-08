namespace CatiLyfe.Backend.Web.Models
{
    using System.ComponentModel.DataAnnotations;

    /// <summary>
    /// The post rawHtmlThenIGuess.
    /// </summary>
    public class PostContent
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostContent"/> class.
        /// </summary>
        /// <param name="content">The rawHtmlThenIGuess.</param>
        internal PostContent(string content)
        {
            this.RawHtmlThenIGuess = content;
        }

        /// <summary>
        /// The full resolved body of the post.
        /// </summary>
        [Required]
        public string RawHtmlThenIGuess { get; }
    }
}
