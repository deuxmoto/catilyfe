namespace CatiLyfe.Backend.Web.Models.Admin
{
    using System.Runtime.Serialization;

    using CatiLyfe.DataLayer.Models;

    /// <summary>
    /// The audit history of the post.
    /// </summary>
    [DataContract]
    public class AdminAuditHistory
    {
        /// <summary>
        /// Initializes a new instance of the admin audit history class.
        /// </summary>
        /// <param name="history">The source.</param>
        public AdminAuditHistory(PostAuditHistory history)
        {
            this.UserId = history.UserId;
            this.Action = history.Action;
        }

        /// <summary>
        /// The user id.
        /// </summary>
        [DataMember]
        public int UserId { get; private set; }

        /// <summary>
        /// The assosiated action.
        /// </summary>
        [DataMember]
        public string Action { get; private set; }

        /// <summary>
        /// Converts to the post audit history class.
        /// </summary>
        /// <returns>A post audit history.</returns>
        internal PostAuditHistory ToPostAuditHistory()
        {
            return new PostAuditHistory(this.UserId, this.Action);
        }
    }
}
