namespace CatiLyfe.DataLayer.Models
{
    /// <summary>
    /// The user role description.
    /// </summary>
    public class UserRoleDescription
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleDescription"/> class.
        /// </summary>
        /// <remarks>Not for you. For the serializer.</remarks>
        public UserRoleDescription()
        {
            // for the stupid serializer.
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserRoleDescription"/> class.
        /// </summary>
        /// <param name="roleName">The role name.</param>
        /// <param name="description">The description.</param>
        public UserRoleDescription(string roleName, string description)
        {
            this.RoleName = roleName;
            this.Description = description;
        }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets the role name.
        /// </summary>
        public string RoleName { get; set; }
    }
}
