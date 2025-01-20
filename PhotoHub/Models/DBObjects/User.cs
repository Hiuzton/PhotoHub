namespace PhotoHub.Models.DBObjects
{
    public class User
    {
        public Guid IdUser { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; }
    }
}
