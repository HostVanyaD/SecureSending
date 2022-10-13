namespace SecureSending.Models
{
    using Microsoft.EntityFrameworkCore;
    using SecureSending.Models.Entities;


    public class UserService : IUserService
    {
        private const double HoursToLive = 48;
        private const int ClicksAllowed = 2;

        private readonly AppDbContext _context;

        public UserService(AppDbContext context)
        {
            _context = context;
            Seed();
        }

        public async Task RegisterAccountAsync(string username, string password, string key)
        {
            var newAccount = new User()
            {
                Username = username,
                Password = password,
                UniqueKey = key
            };

            await _context.Users.AddAsync(newAccount);

            await SaveAsync();
        }

        public async Task<string[]> GetUserCredentialsByUniqueKeyAsync(string key)
        {
            var users = await _context.Users.ToListAsync();

            var user = users.SingleOrDefault(u => u.UniqueKey == key);

            var timeNow = DateTime.UtcNow;

            var lifeHours = (timeNow - user.KeyCreation).TotalHours;

            if (lifeHours > HoursToLive || user.Clicks >= ClicksAllowed)
            {
                return null;
            }

            user.Clicks += 1;

            _context.SaveChanges();

            return new string[] { user.Username, user.Password };
        }

        public async Task<bool> UserExistsAsync(string username, string password)
        {
            return await _context.Users.AnyAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<bool> UniqueKeyExistsAsync(string key)
        {
            return await _context.Users.AnyAsync(u => u.UniqueKey == key);
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
                    Password = "samplePassword",
                    UniqueKey = "dzBOUVhyYnpLN2J2WjdoRDJsYzU2UXJMdks3T1d0bjVmZFFqRFZQVFY4RFlpMUFCUmYyVVFGSUQ1bmJrSkkwRy9FRWZJcVZOZERKY2ZTN216djZHUjlicENRckdMT1ZNSGVFbGVEZlovOVBwanExMlQ1S2tQa2ViMGhlMzI3WWFycDJJR0E9PQ",
                    KeyCreation = DateTime.UtcNow.AddDays(-4)
                },
                new User()
                {
                    Username = "username2",
                    Password = "samplePassword2",
                    UniqueKey = "kkBOUVhyYnpLN2J2WjdoRDJsYzU2UXJMdks3T1d0bjVmZFFqRFZQVFY4RFlpMUFCUmYyVVFGSUQ1bmJrSkkwRy9FRWZJcVZOZERKY2ZTN216djZHUjlicENRckdMT1ZNSGVFbGVEZlovOVBwanExMlQ1S2tQa2ViMGhlMzI3WWFycDJJR0E9PQ",
                    KeyCreation = DateTime.UtcNow.AddDays(1)
                });

            _context.SaveChanges();
        }
    }
}
