namespace SecureSending.Services.Security
{
    using System.Security.Cryptography;
    using System.Net;
    using Microsoft.IdentityModel.Tokens;

    public class GenerateKey : IKeyGenerator
    {
        public string GetSecureKey()
        {
            var randomNumber = new byte[150];
            string randomString = string.Empty;

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                randomString = Convert.ToBase64String(randomNumber);
            }

            var keyEncoded = Base64UrlEncoder.Encode(randomString);

            return keyEncoded;
        }

    }
}
