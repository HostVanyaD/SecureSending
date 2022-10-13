namespace SecureSending.Models
{
    using Microsoft.EntityFrameworkCore;
    using SecureSending.Models.Entities;

    using static SecureSending.Models.DbConstants;

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

        public async Task<(bool, string)> RegisterAccountAsync(string username, string password)
        {
            var userExists = await UserExistsAsync(username, password);

            if (userExists)
            {
                return (false, AccountAlreadyExistsMessage);
            }

            var newAccount = new User()
            {
                Username = username,
                Password = password
            };

            await _context.Users.AddAsync(newAccount);

            await SaveAsync();

            return (true, AccountRegisteredMessage);
        }

        public async Task<User> GetByUsernameAndPassAsync(string username, string password)
        {
            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<(string, string)> GetUserCredentialsByUniqueKeyAsync(string key)
        {
            var users = await _context.Users.ToListAsync();

            var user = users.SingleOrDefault(u => u.UniqueKey == key);

            return (user.Username, user.Password);
        }

        public async Task<bool> UserExistsAsync(string username, string password)
        {
            return await _context.Users.AnyAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<bool> UniqueKeyExistsAsync(string key)
        {
            return await _context.Users.AnyAsync(u => u.UniqueKey == key);
        }

        public async Task<(bool, string)> SetAccountKeyAsync(string username, string password, string uniqueKey)
        {
            var user = await GetByUsernameAndPassAsync(username, password);

            if (user == null)
            {
                return (false, AccountNotFoundMessage);
            }

            var keyExists = await UniqueKeyExistsAsync(uniqueKey);

            if (keyExists)
            {
                return (false, KeyAlreadyExistsMessage);
            }

            user.UniqueKey = uniqueKey;

            await SaveAsync();

            return (true, KeySetSuccessfulMessage);
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
