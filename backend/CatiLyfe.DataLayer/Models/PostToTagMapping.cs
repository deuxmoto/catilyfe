namespace CatiLyfe.DataLayer.Models
{
    /// <summary>
    /// The post to tag mapping.
    /// </summary>
    internal class PostToTagMapping
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PostToTagMapping"/> class.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="tag">The tag.</param>
        public PostToTagMapping(int postId, string tag)
        {
            this.PostId = postId;
            this.Tag = tag;
        }

        /// <summary>
        /// Gets the tag.
        /// </summary>
        public string Tag { get; }

        /// <summary>
        /// Gets the post id.
        /// </summary>
        public int PostId { get; }
    }
}
