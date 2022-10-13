namespace SecureSending.Services.Account
{
    using SecureSending.DTO;
    using SecureSending.Models;
    using SecureSending.Services.Security;

    public class AccountService : IAccountService
    {
        private readonly IUserService _accounts;
        private readonly IKeyGenerator _keyGenerator;

        public AccountService(IUserService accounts, IKeyGenerator keyGenerator)
        {
            _accounts = accounts;
            _keyGenerator = keyGenerator;
        }

        public async Task<(bool, string)> RegisterAccountAsync(CredentialsDto credentials)
        {
            return await _accounts.RegisterAccountAsync(credentials.Username, credentials.Password);
        }

        public async Task<(bool, string)> GenerateUniqueKeyAsync(CredentialsDto credentials)
        {
            var uniqueKey = _keyGenerator.GetSecureKey();

            var (successful, message) = await _accounts.SetAccountKeyAsync(credentials.Username, credentials.Password, uniqueKey);

            return (successful, message);
        }

        public async Task<CredentialsDto> GetCredentialsByKey(string key)
        {
            var credentials = await _accounts.GetUserCredentialsByUniqueKeyAsync(key);

            var model = new CredentialsDto()
            {
                Username = credentials.Item1,
                Password = credentials.Item2
            };

            return model;
        }
    }
}
