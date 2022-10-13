namespace SecureSending.Services.Account
{
    using SecureSending.DTO;

    public interface IAccountService
    {
        public Task<(bool, string)> RegisterAccountAsync(CredentialsDto credentials);

        public Task<(bool, string)> GenerateUniqueKeyAsync(CredentialsDto credentials);

        public Task<CredentialsDto> GetCredentialsByKey(string key);
    }
}
