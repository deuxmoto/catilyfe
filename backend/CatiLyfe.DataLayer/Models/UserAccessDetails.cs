namespace CatiLyfe.DataLayer.Models
{
    /// <summary>
    /// The user access details.
    /// </summary>
    public class UserAccessDetails
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserAccessDetails"/> class.
        /// </summary>
        /// <param name="userid"></param>
        public UserAccessDetails(int userid, string token)
        {
            this.UserId = userid;
            this.Token = token;
        }

        /// <summary>
        /// Gets the user id.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Gets the user token.
        /// </summary>
        public string Token { get; }
    }
}
