namespace CatiLyfe.Backend.Web.Models.Login
{
    using System.Runtime.Serialization;

    /// <summary>
    /// The login details.
    /// </summary>
    [DataContract]
    public class LoginDetails
    {
        [DataMember]
        public string Token { get; set; }
    }
}