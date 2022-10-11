namespace SecureSending.Data
{
    using Microsoft.EntityFrameworkCore;
    using SecureSending.Data.Models;

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<User> Users { get; set; }
    }
}
