namespace SecureSending.Models
{
    using Microsoft.EntityFrameworkCore;
    using SecureSending.Models.Entities;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
