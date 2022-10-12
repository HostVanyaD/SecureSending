namespace SecureSending.Services.Data
{
    using Microsoft.EntityFrameworkCore;
    using SecureSending.Data;
    using SecureSending.Data.Models;

    public class DbService : IDbService
    {
        //private readonly Repository _repo;

        //public DbService(Repository repo)
        //{
        //    _repo = repo;
        //}

        private readonly AppDbContext _context;

        public DbService(AppDbContext repo)
        {
            _context = repo;
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            //return await _repo.All<User>().ToListAsync();

            return await _context.Users.ToListAsync();
        }

        public async Task<User> GetByUsernameAndPassAsync(string username, string password)
        {
            //return await _repo.All<User>().SingleOrDefaultAsync(u => u.Username == username && u.Password == password);

            return await _context.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<User> GetUserByUniqueLinkAsync(string uniqueLink)
        {
            //return await _repo.All<User>().SingleOrDefaultAsync(u => u.Link.Contains(uniqueLink));
            var user = await _context.Users.SingleOrDefaultAsync(u => u.Link == uniqueLink);

            return user;
        }

        public async Task Save()
        {
            //await _repo.SaveChangesAsync();
            await _context.SaveChangesAsync();
        }

        //public async Task Seed()
        //{
        //    if (_repo.All<User>().Any())
        //    {
        //        return;
        //    }

        //    await _repo.AddRangeAsync<User>(new List<User>
        //    {
        //        new User()
        //        {
        //            Username = "username1",
        //            Password = "samplePassword"
        //        },
        //        new User()
        //        {
        //            Username = "username2",
        //            Password = "samplePassword2"
        //        }
        //    });

        //    await _repo.SaveChangesAsync();
        //}
    }
}
