namespace CatiLyfe.Backend.Web.Models.User
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;

    [DataContract]
    public class User
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public IEnumerable<string> Roles { get; set; }
    }
}