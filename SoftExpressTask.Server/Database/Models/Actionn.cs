namespace SoftExpressTask.Server.Database.Models
{
    public class Actionn
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string DeviceType { get; set; }
        public string Region { get; set; }
        public string Application { get; set; }
        public DateTime Timedate { get; set; }
        public User User { get; set; }
    }
}
