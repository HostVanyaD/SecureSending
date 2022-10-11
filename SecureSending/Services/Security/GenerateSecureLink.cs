﻿namespace SecureSending.Services.Security
{
    using System.Security.Cryptography;
    using System.Net;

    public class GenerateSecureLink : IGenerateSecureLink
    {
        public string GetSecureExtension()
        {
            var randomNumber = new byte[150];
            string randomString = string.Empty;

            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomNumber);
                randomString = Convert.ToBase64String(randomNumber);
            }

            var keyEncoded = WebUtility.UrlEncode(randomString);

            return keyEncoded;
        }

    }
}
