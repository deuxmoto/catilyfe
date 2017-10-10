﻿namespace CatiLyfe.DataLayer.Models
{
    /// <summary>
    /// The user model.
    /// </summary>
    public class UserModel
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="name">The name.</param>
        /// <param name="email">The email.</param>
        /// <param name="password">The user password.</param>
        public UserModel(int id, string name, string email, byte[] password)
        {
            this.Id = id;
            this.Name = name;
            this.Email = email;
            this.Password = password;
        }

        /// <summary>
        /// Gets the password.
        /// </summary>
        public byte[] Password { get; }

        /// <summary>
        /// Gets the id.
        /// </summary>
        public int Id { get; }

        /// <summary>
        /// Gets the name.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Gets the email.
        /// </summary>
        public string Email { get; }
    }
}
