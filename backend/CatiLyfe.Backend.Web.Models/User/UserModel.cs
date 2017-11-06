namespace CatiLyfe.Backend.Web.Models.User
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.Runtime.Serialization;

    using CatiLyfe.Common.Security;
    using CatiLyfe.DataLayer.Models;

    [DataContract]
    public class UserModel
    {
        /// <summary>
        /// Empty constructor for stupid json.
        /// </summary>
        public UserModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="UserModel"/> class.
        /// </summary>
        /// <param name="user">The user.</param>
        public UserModel(User user)
        {
            this.Id = user.Id;
            this.Name = user.Name;
            this.Email = user.Email;
            this.Password = null;
            this.Roles = user.Roles;
        }

        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        [MinLength(4)]
        [MaxLength(64)]
        public string Name { get; set; }

        [DataMember]
        [MaxLength(128)]
        [MinLength(8)]
        public string Email { get; set; }

        [DataMember]
        [MinLength(4)]
        public string Password { get; set; }

        [DataMember]
        public IEnumerable<string> Roles { get; set; }

        /// <summary>
        /// Convert a user to 
        /// </summary>
        /// <param name="passwordHelper">The password helper.</param>
        /// <returns>The user.</returns>
        public User ToUser(IPasswordHelper passwordHelper)
        {
            var password = string.IsNullOrWhiteSpace(this.Password)
                               ? null
                               : passwordHelper.HashPassword(this.Password);

            return new User(this.Id, this.Name, this.Email, password, new HashSet<string>(this.Roles));
        }
    }
}