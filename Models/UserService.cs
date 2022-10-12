namespace SecureSending.Models
{
    using Microsoft.EntityFrameworkCore;
    using SecureSending.Models.Entities;

    public class UserService : IUserService
    {
        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
            Seed();
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByUsernameAndPassAsync(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User> GetByUniqueKeyAsync(string uniqueLink)
        {
            var users = await _context.Users.ToListAsync();

            var user = users.Where(u => u.UniqueKey.Contains(uniqueLink)).FirstOrDefault();

            return user;
        }

        public async Task<bool> UserExistsAsync(string username, string password)
        {
            return await _context.Users.AnyAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<bool> UniqueKeyExistsAsync(string key)
        {
            return await _context.Users.AnyAsync(u => u.UniqueKey == key);
        }

        public async Task<(bool, string)> SetAccountKey(string username, string password, string uniqueKey)
        {
            var user = await this.GetByUsernameAndPassAsync(username, password);

            if (user == null)
            {
                return (false, "Account doesn't exist.");
            }

            var keyExists = await this.UniqueKeyExistsAsync(uniqueKey);

            if (keyExists)
            {
                return (false, "Key already exists. Please try to generate another key.");
            }

            user.UniqueKey = uniqueKey;

            await this.SaveAsync();

            return (true, "Account key has been set successfully");
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }

        public void Seed()
        {
            if (_context.Users.Any())
            {
                return;
            }

            _context.AddRange(
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
