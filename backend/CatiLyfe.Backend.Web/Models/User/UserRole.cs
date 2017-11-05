namespace CatiLyfe.Backend.Web.Models.User
{
    using CatiLyfe.DataLayer.Models;

    /// <summary>
    /// A user role.
    /// </summary>
    public class UserRole
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRole"/> class.
        /// </summary>
        /// <param name="role">The role.</param>
        internal UserRole(UserRoleDescription role)
        {
            this.Role = role.RoleName;
            this.Description = role.Description;
        }

        /// <summary>
        /// Gets the name of the role.
        /// </summary>
        public string Role { get; }

        /// <summary>
        /// Gets the description of the role.
        /// </summary>
        public string Description { get; }
    }
}