namespace SecureSending.Services.Data
{
    using SecureSending.Data.Models;

    public interface IDbService
    {
        public Task<IEnumerable<User>> GetAllAsync();

        public Task<User> GetByUsernameAndPassAsync(string username, string password);

        public Task<User> GetUserByUniqueLinkAsync(string uniqueLink);

        public Task Save();
    }
}
