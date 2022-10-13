namespace SecureSending.Models
{
    using SecureSending.Models.Entities;

    public interface IUserService
    {
        public Task RegisterAccountAsync(string username, string password, string key);

        public Task<string[]> GetUserCredentialsByUniqueKeyAsync(string key);

        public Task<bool> UserExistsAsync(string username, string password);

        public Task<bool> UniqueKeyExistsAsync(string key);

        public Task SaveAsync();
    }
}
