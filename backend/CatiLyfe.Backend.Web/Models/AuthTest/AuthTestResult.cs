namespace CatiLyfe.Backend.Web.Models.AuthTest
{
    using System.Collections.Generic;

    /// <summary>
    /// The auth test result.
    /// </summary>
    public class AuthTestResult
    {
        /// <summary>
        /// Initializes a new AuthTestResult.
        /// </summary>
        /// <param name="message">The message.</param>
        public AuthTestResult(string message, string userName, IEnumerable<string> roles)
        {
            this.Message = message;
            this.Name = userName;
            this.Roles = roles;
        }

        /// <summary>
        /// Gets the user roles.
        /// </summary>
        public IEnumerable<string> Roles { get; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        public string Name { get; }
    }
}