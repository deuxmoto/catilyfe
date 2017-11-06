namespace CatiLyfe.Backend.Web.Models.Login
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The login credentials.
    /// </summary>
    [DataContract]
    public class LoginCredentials
    {
        /// <summary>
        /// The email.
        /// </summary>
        [DataMember]
        public string Email { get; set; }

        /// <summary>
        /// The password.
        /// </summary>
        [DataMember]
        public string Password { get; set; }
    }
}