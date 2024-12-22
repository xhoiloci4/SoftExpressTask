namespace SoftExpressTask.Server.Database.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Role { get; set; }
        public string PasswordHash { get; set; }
        public ICollection<Actionn> Actions { get; set; }
    }

    public enum UserRole
    {
        Admin,
        User,
    }
}
