using Microsoft.EntityFrameworkCore;
using SoftExpressTask.Server.Database.Models;
namespace SoftExpressTask.Server.Database

{
    public class AppDbContext : DbContext
    {
        public  AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Actionn> Actions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Actions)
                .WithOne(u => u.User)
                .HasForeignKey(u => u.UserId);
            base.OnModelCreating(modelBuilder);
        }

    }
}
