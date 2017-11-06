namespace CatiLyfe.DataLayer.Models
{
    using System.Collections.Generic;

    /// <summary>
    /// The user model.
    /// </summary>
    public class User
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="User"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The user password.</param>
        /// <param name="roles">The user roles.</param>
        public User(int? id, string name, string email, byte[] password, HashSet<string> roles = null)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
            this.Roles = roles;
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public byte[] Password { get; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public int? Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        public string Email { get; }

        /// <summary>
        /// Gets the roles.
        /// </summary>
        public HashSet<string> Roles { get; set; }
    }
}
