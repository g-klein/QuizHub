using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using QuizHub.Models.Auth;

namespace QuizHub.Auth
{
    public class QuizPasswordHash : IHashPasswords, IVerifyPasswords
    {
        public PasswordHashResult HashPassword(string password)
        {
            var salt = GenerateSalt();
            var hash = GetHash(password, salt);

            return new PasswordHashResult() { HashedPassword = hash, Salt = salt };
        }

        public bool IsPasswordValid(string password, string salt, string hash)
        {
            var recomputedHash = GetHash(password, salt);
            return recomputedHash == hash;
        }

        internal string GenerateSalt()
        {
            using (RNGCryptoServiceProvider saltGenerator = new RNGCryptoServiceProvider())
            {
                byte[] salt = new byte[16];
                saltGenerator.GetBytes(salt);


                return Convert.ToBase64String(salt);
            }
        }

        internal string GetHash(string password, string salt)
        {
            byte[] passwordBytes = Encoding.ASCII.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(passwordBytes, saltBytes, 10000);
            byte[] hash = pbkdf2.GetBytes(20);

            byte[] hashBytes = new byte[36];
            Array.Copy(saltBytes, 0, hashBytes, 0, 16);
            Array.Copy(hash, 0, hashBytes, 16, 20);

            return Convert.ToBase64String(hashBytes);
        }
    }
}
