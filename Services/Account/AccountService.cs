namespace SecureSending.Services.Account
{
    using SecureSending.DTO;
    using SecureSending.Models;
    using SecureSending.Services.Security;

    public class AccountService
    {
        private readonly IUserService _accounts;
        private readonly IKeyGenerator _keyGenerator;

        public AccountService(IUserService accounts, IKeyGenerator keyGenerator)
        {
            _accounts = accounts;
            _keyGenerator = keyGenerator;
        }

        public async Task GenerateUniqueKey(CredentialsDto credentials)
        {
            var uniqueKey = _keyGenerator.GetSecureKey();

            var keySetSuccessful = (await _accounts.SetAccountKey(credentials.Username, credentials.Password, uniqueKey)).Item1;
        }
    }
}
