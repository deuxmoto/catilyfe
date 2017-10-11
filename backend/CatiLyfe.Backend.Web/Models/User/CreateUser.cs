namespace CatiLyfe.Backend.Web.Models.User
{
    /// <summary>
    /// The create user model.
    /// </summary>
    public class CreateUser
    {
        public string Email { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }
    }
}