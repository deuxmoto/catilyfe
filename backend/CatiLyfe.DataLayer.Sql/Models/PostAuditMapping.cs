namespace CatiLyfe.DataLayer.Sql.Models
{
    using CatiLyfe.DataLayer.Models;

    /// <summary>
    /// A post to audit mapping.
    /// </summary>
    internal class PostAuditMapping
    {
        /// <summary>
        /// Initializes an instance of the <see cref="PostAuditMapping"/> class.
        /// </summary>
        /// <param name="postId">The post id.</param>
        /// <param name="userId">The user id.</param>
        /// <param name="action">The action.</param>
        public PostAuditMapping(int postId, int userId, string action)
        {
            this.PostId = postId;
            this.UserId = userId;
            this.Action = action;
        }

        /// <summary>
        /// Gets the post id.
        /// </summary>
        public int PostId { get; }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Gets the action assosiated.
        /// </summary>
        public string Action { get; }

        /// <summary>
        /// Convert this object to a post audit history.
        /// </summary>
        /// <returns>The post audit history.</returns>
        public PostAuditHistory ToPostAuditHistory()
        {
            return new PostAuditHistory(this.UserId, this.Action);
        }
    }
}
