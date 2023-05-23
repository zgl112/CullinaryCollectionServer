using System.Collections;
using System.Security.Cryptography;
using System;

namespace baseAPI.Utils
{
    public static class HashHelper
    {
        public static string GenerateSalt()
        {
            var salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string saltString)
        {
            var salt = Convert.FromBase64String(saltString);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                var hash = pbkdf2.GetBytes(20);
                var combinedHash = new byte[hash.Length + salt.Length];
                Array.Copy(hash, 0, combinedHash, 0, hash.Length);
                Array.Copy(salt, 0, combinedHash, hash.Length, salt.Length);
                return Convert.ToBase64String(combinedHash);
            }
        }

        public static bool VerifyPassword(string password, string saltString, string hashedPassword)
        {
            var salt = Convert.FromBase64String(saltString);
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            var hash = new byte[20];
            Array.Copy(hashedPasswordBytes, 0, hash, 0, 20);
            var saltInHash = new byte[salt.Length];
            Array.Copy(hashedPasswordBytes, 20, saltInHash, 0, salt.Length);
            return StructuralComparisons.StructuralEqualityComparer.Equals(salt, saltInHash) &&
                StructuralComparisons.StructuralEqualityComparer.Equals(hash,
                new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(20));
        }
    }
}

