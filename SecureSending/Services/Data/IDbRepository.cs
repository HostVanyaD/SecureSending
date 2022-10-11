namespace SecureSending.Services.Data
{
    using SecureSending.Data.Models;

    public interface IDbRepository
    {
        public Task<IEnumerable<User>> GetAllAsync();

        public Task<User> GetByIdAsync(string id);

        public Task<User> GetUserCredentialsByUniqueLinkAsync(string uniqueLink);

        public void Save();
    }
}
