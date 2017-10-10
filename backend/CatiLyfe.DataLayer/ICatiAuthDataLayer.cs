namespace CatiLyfe.DataLayer
{
    using System;
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
        Task<UserModel> GetUser(int? id, string email, byte[] token);

        /// <summary>
        /// Creates a token for a user.
        /// </summary>
        /// <param name="user">The user id.</param>
        /// <param name="token">The token.</param>
        /// <param name="expiry">The expiry.</param>
        /// <returns>An async task.</returns>
        Task CreateToken(int user, byte[] token, DateTime expiry);
    }
}
