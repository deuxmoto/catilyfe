namespace CatiLyfe.DataLayer
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using CatiLyfe.DataLayer.Models;

    /// <summary>
    /// The CatiAuthDataLayer interface.
    /// </summary>
    public interface ICatiAuthDataLayer
    {
        /// <summary>
        /// Gets all user information.
        /// </summary>
        /// <param name="id">The id.</param>
        /// <param name="email">The email.</param>
        /// <param name="token">The token.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task<IEnumerable<User>> GetUser(int? id, string email, byte[] token);

        /// <summary>
        /// Creates a token for a user.
        /// </summary>
        /// <param name="user">The user id.</param>
        /// <param name="token">The token.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>An async task.</returns>
        Task CreateToken(int user, byte[] token, DateTime expiry);

        /// <summary>
        /// Sets a user based on the user model.
        /// </summary>
        /// <param name="usermodel">The user model.</param>
        /// <returns>An async task..</returns>
        Task SetUser(User usermodel);

        /// <summary>
        /// Gets the user roles.
        /// </summary>
        /// <returns>The list of roles.</returns>
        Task<IEnumerable<UserRoleDescription>> GetRoles();

        /// <summary>
        /// Deletes a role.
        /// </summary>
        /// <param name="name">The name of role.</param>
        /// <param name="commit">To really delete it. Or just soft delete.</param>
        /// <returns>The task.</returns>
        Task DeleteRole(string name, bool commit);

        /// <summary>
        /// Creates or edits a role.
        /// </summary>
        /// <param name="role">The role.</param>
        /// <returns>The <see cref="Task"/>.</returns>
        Task SetRole(UserRoleDescription role);
    }
}
