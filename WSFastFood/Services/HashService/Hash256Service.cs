using System.Security.Cryptography;
using System.Text;

namespace WSFastFood.Services.HashService
{
    public class Hash256Service : IHashService
    {
        private readonly IConfiguration _configuration;

        public Hash256Service(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void CreateHashPassword(string password, out string hashPasswordSalt, out string salt)
        {
            IConfigurationSection text = _configuration.GetSection("AppSettings:SecretSalt");
            Console.WriteLine(text.Value!.ToString());
            salt = Hash(DateTime.Now.ToString() + text.Value.ToString());
            hashPasswordSalt = Hash($"{password}{salt}");
        }

        public bool VerifyHashPassword(string password, string salt, string hashPasswordSalt)
        {
            string hashLoginPassword = Hash($"{password}{salt}");

            if (hashLoginPassword == hashPasswordSalt)
            {
                return true;
            }
            return false;
        }


        private static string Hash(string toHash)
        {
            SHA256 sha256 = SHA256.Create();
            byte[] hashBytes = Encoding.Default.GetBytes(toHash);
            byte[] hashed = sha256.ComputeHash(hashBytes);
            string hashedString = Convert.ToHexString(hashed);

            return hashedString;
        }
    }
}
