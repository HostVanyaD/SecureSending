namespace SecureSending.Services.Account
{
    using SecureSending.DTO;

    public interface IAccountService
    {
        public Task<string> RegisterAccountAsync(CredentialsDto credentials);

        public Task<CredentialsDto> GetCredentialsByKey(string key);
    }
}
