namespace Drinks.Services
{
    using System.IO;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    public interface IPasswordHelper
    {
        byte[] GenerateHashedPassword(string password, out byte[] salt);
        bool ValidatePassword(string passwordToValidate, byte[] salt, byte[] hashedPassword);
        byte[] GenerateSalt();
    }

    public class PasswordHelper : IPasswordHelper
    {
        public byte[] GenerateHashedPassword(string password, out byte[] salt)
        {
            salt = GenerateSalt();
            return Hash(password, salt);
        }

        public bool ValidatePassword(string passwordToValidate, byte[] salt, byte[] hashedPassword)
        {
            return Hash(passwordToValidate, salt).SequenceEqual(hashedPassword);
        }

        public byte[] GenerateSalt()
        {
            var rngCrypto = new RNGCryptoServiceProvider();
            var saltBytes = new byte[64];
            rngCrypto.GetNonZeroBytes(saltBytes);
            return saltBytes;
        }

        // ReSharper disable once ParameterTypeCanBeEnumerable.Local
        static byte[] Hash(string password, byte[] salt)
        {
            var passwordAndHash = Encoding.Default.GetBytes(password).Concat(salt).ToArray();
            using (var sha512 = SHA512.Create())
            {
                byte[] hashBytes;
                using (var passwordStream = new MemoryStream(password.Length))
                {
                    using (var writer = new StreamWriter(passwordStream))
                    {
                        writer.Write(password);
                        writer.Flush();
                        hashBytes = sha512.ComputeHash(passwordAndHash);
                    }
                }

                return hashBytes;
            }
        }
    }
}