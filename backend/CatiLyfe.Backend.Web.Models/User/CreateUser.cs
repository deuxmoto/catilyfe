namespace CatiLyfe.Backend.Web.Models.User
{
    using System.Collections.Generic;

    /// <summary>
    /// The create user model.
    /// </summary>
    public class CreateUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}