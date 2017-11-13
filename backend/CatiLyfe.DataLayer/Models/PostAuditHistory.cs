namespace CatiLyfe.DataLayer.Models
{
    public class PostAuditHistory
    {
        /// <summary>
        /// Gets the post audit history.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="action">The action.</param>
        public PostAuditHistory(int userId, string action)
        {
            this.UserId = userId;
            this.Action = action;
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Gets the action.
        /// </summary>
        public string Action { get; }
    }
}
