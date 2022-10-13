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

        public async Task<string> RegisterAccountAsync(CredentialsDto credentials)
        {
            var userExists = await _accounts.UserExistsAsync(credentials.Username, credentials.Password);

            var uniqueKey = _keyGenerator.GetSecureKey();

            var keyExists = await _accounts.UniqueKeyExistsAsync(uniqueKey);

            if (userExists || keyExists)
            {
                return null;
            }

            await _accounts.RegisterAccountAsync(credentials.Username, credentials.Password, uniqueKey);

            return uniqueKey;
        }

        public async Task<CredentialsDto> GetCredentialsByKey(string key)
        {
            var credentials = await _accounts.GetUserCredentialsByUniqueKeyAsync(key);

            if (credentials == null)
            {
                return null;
            }

            var model = new CredentialsDto()
            {
                Username = credentials[0],
                Password = credentials[1]
            };

            return model;
        }
    }
}
