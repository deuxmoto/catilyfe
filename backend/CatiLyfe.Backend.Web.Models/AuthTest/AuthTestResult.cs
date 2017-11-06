namespace CatiLyfe.Backend.Web.Models.AuthTest
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    /// <summary>
    /// The auth test result.
    /// </summary>
    [DataContract]
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
        [DataMember]
        public IEnumerable<string> Roles { get; private set; }

        /// <summary>
        /// Gets the message.
        /// </summary>
        [DataMember]
        public string Message { get; private set; }

        /// <summary>
        /// Gets the user name.
        /// </summary>
        [DataMember]
        public string Name { get; private set; }
    }
}