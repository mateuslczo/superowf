using Microsoft.IdentityModel.Tokens;
using System.Security.Cryptography;

namespace TaskList._01___Application.TokenSecurity
{

    public class TokenSecurityConfig
    {

        public SecurityKey Key { get; }
        public SigningCredentials Credentials { get; }

        public TokenSecurityConfig()
        {
            using (var provider = new RSACryptoServiceProvider(2048))
            {
                Key = new RsaSecurityKey(provider.ExportParameters(true));
            }

            Credentials =
                new SigningCredentials
                (
                    Key,
                    SecurityAlgorithms.RsaSha256Signature
                );
        }
    }
}
