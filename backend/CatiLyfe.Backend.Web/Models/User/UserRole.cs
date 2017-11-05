namespace CatiLyfe.Backend.Web.Models.User
{
    using System.Runtime.Serialization;

    using CatiLyfe.DataLayer.Models;

    /// <summary>
    /// A user role.
    /// </summary>
    [DataContract]
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
        [DataMember]
        public string Role { get; private set; }

        /// <summary>
        /// Gets the description of the role.
        /// </summary>
        [DataMember]
        public string Description { get; private set; }
    }
}