namespace CatiLyfe.DataLayer.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.Data.SqlClient;
    using System.Runtime.CompilerServices;

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
        [MaxLength(512)]
        [Required]
        public string Description { get; set; }

        /// <summary>
        /// Gets the role name.
        /// </summary>
        [MaxLength(64)]
        [Required]
        public string RoleName { get; set; }

        /// <summary>
        /// The from reader.
        /// </summary>
        /// <param name="reader">The reader.</param>
        /// <returns>The <see cref="UserRoleDescription"/>.</returns>
        public static UserRoleDescription FromReader(SqlDataReader reader)
        {
            return new UserRoleDescription((string)reader["role"], (string)reader["description"]);
        }
    }
}
