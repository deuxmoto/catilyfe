namespace CatiLyfe.Backend.Web.Models.User
{
    using System.Collections.Generic;

    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; }

        public IEnumerable<string> Roles { get; set; }
    }
}