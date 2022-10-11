namespace SecureSending.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SecureSending.Data;
    using SecureSending.Data.Models;

    public class DbRepository : IDbRepository
    {
        private readonly AppDbContext _context;

        public DbRepository(AppDbContext context)
        {
            _context = context;
            Seed();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByIdAsync(string id)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetUserCredentialsByUniqueLinkAsync(string uniqueLink)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Link.Contains(uniqueLink));
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Seed()
        {
            if (_context.Users.Any())
            {
                return;
            }

            _context.Users.AddRange(
                new User()
                {
                    Username = "username1",
                    Password = "samplePassword"
                },
                new User()
                {
                    Username = "username2",
                    Password = "samplePassword2"
                });

            _context.SaveChanges();
        }
    }
}
