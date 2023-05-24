using System.Collections;
using System.Security.Cryptography;
using System;

namespace baseAPI.Utils
{
    public static class HashHelper
    {
        public static string GenerateSalt()
        {
            // Generate a random salt of 32 bytes
            var salt = new byte[32];
            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(salt);
            }
            // Convert the salt to a base64 string and return it
            return Convert.ToBase64String(salt);
        }

        public static string HashPassword(string password, string saltString)
        {
            // Convert the salt from the base64 string to a byte array
            var salt = Convert.FromBase64String(saltString);
            using (var pbkdf2 = new Rfc2898DeriveBytes(password, salt, 10000))
            {
                // Generate the hash of the password using the salt
                var hash = pbkdf2.GetBytes(20);
                // Combine the hash and salt into a single byte array
                var combinedHash = new byte[hash.Length + salt.Length];
                Array.Copy(hash, 0, combinedHash, 0, hash.Length);
                Array.Copy(salt, 0, combinedHash, hash.Length, salt.Length);
                // Convert the combined hash to a base64 string and return it
                return Convert.ToBase64String(combinedHash);
            }
        }

        public static bool VerifyPassword(string password, string saltString, string hashedPassword)
        {
            // Convert the salt and hashed password from base64 strings to byte arrays
            var salt = Convert.FromBase64String(saltString);
            var hashedPasswordBytes = Convert.FromBase64String(hashedPassword);
            // Extract the hash and salt from the hashed password byte array
            var hash = new byte[20];
            Array.Copy(hashedPasswordBytes, 0, hash, 0, 20);
            var saltInHash = new byte[salt.Length];
            Array.Copy(hashedPasswordBytes, 20, saltInHash, 0, salt.Length);
            // Compare the salt and hash with the newly generated hash and salt for the given password
            return StructuralComparisons.StructuralEqualityComparer.Equals(salt, saltInHash) &&
                StructuralComparisons.StructuralEqualityComparer.Equals(hash,
                new Rfc2898DeriveBytes(password, salt, 10000).GetBytes(20));
        }

    }
}

