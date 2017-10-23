namespace CatiLyfe.DataLayer.Models
{
    /// <summary>
    /// The user role.
    /// </summary>
    internal class UserRole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <param name="role">The role.</param>
        public UserRole(int userId, string role)
        {
            this.UserId = userId;
            this.Role = role;
        }

        /// <summary>
        /// Gets or sets the user id.
        /// </summary>
        public int UserId { get; }

        /// <summary>
        /// Gets the role.
        /// </summary>
        public string Role { get; }
    }
}
