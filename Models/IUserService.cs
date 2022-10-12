namespace SecureSending.Models
{
    using SecureSending.Models.Entities;

    public interface IUserService
    {
        public Task<IEnumerable<User>> GetAllAsync();

        public Task<User> GetByUsernameAndPassAsync(string username, string password);

        public Task<User> GetByUniqueKeyAsync(string key);

        public Task<bool> UserExistsAsync(string username, string password);

        public Task<bool> UniqueKeyExistsAsync(string key);

        public Task<(bool, string)> SetAccountKey(string username, string password, string uniqueKey);

        public Task SaveAsync();
    }
}
