using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;

namespace ChatR.Helpers.Hash
{
    public class HashGenerator
    {

        public const int SALT_BYTE_SIZE = 32;
        public const int HASH_BYTE_SIZE = 32;
        public const int PBKDF2_ITERATIONS = 4096;

        /// <summary>
        /// Generates hash (SHA256) for the provided string.
        /// </summary>
        /// <param name="value">The string to hash.</param>
        /// <returns>The hash of the provided string.</returns>
        public static string CreateHash(string value)
        {
            byte[] salt = GenerateSalt();

            // Hash the password and encode the parameters using SHA256
            using (var hmac = new HMACSHA256())
            {
                var hash = hmac.ComputeHash(GetBytesFromString(value));
                return BitConverter.ToString(hash);
            }
        }
        /// <summary>
        /// Generates SHA256 hash salted with PBKDF2.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <param name="salt">The salt.</param>
        /// <returns></returns>
        public static string CreatePasswordHash(string password, byte[] salt)
        {
            // Hash the password and encode the parameters
            using (var hmac = new HMACSHA256())
            {
                var df = new Pbkdf2(hmac, GetBytesFromString(password), salt, PBKDF2_ITERATIONS);
                return BitConverter.ToString(df.GetBytes(32));
            }
        }

        /// <summary>
        /// Generates random Salt using RNGCryptoServiceProvider
        /// </summary>
        /// <returns>byte[]</returns>
        public static byte[] GenerateSalt()
        {
            RNGCryptoServiceProvider csprng = new RNGCryptoServiceProvider();
            byte[] salt = new byte[SALT_BYTE_SIZE];
            csprng.GetBytes(salt);

            return salt;
        }

        private static byte[] GetBytesFromString(string value)
        {
            byte[] bytes = new byte[value.Length * sizeof(char)];
            System.Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);
            return bytes;
        }
        /// <summary>
        /// Converts password hexadecimal string to ByteArray.
        /// </summary>
        /// <param name="hex">hexadecimal string.</param>
        /// <returns>byte array</returns>
        public static byte[] PasswordHexStringToByteArray(string hex)
        {
            hex = hex.Replace("-", "");

            return Enumerable.Range(0, hex.Length)
                             .Where(x => x % 2 == 0)
                             .Select(x => Convert.ToByte(hex.Substring(x, 2), 16))
                             .ToArray();
        }

        /// <summary>
        /// Compares two hashes
        /// </summary>
        /// <param name="first_hash">first hash</param>
        /// <param name="second_hash">second hash</param>
        /// <returns>bool</returns>
        public static bool SlowEquals(byte[] first_hash, byte[] second_hash)
        {
            uint diff = (uint)first_hash.Length ^ (uint)second_hash.Length;
            for (int i = 0; i < first_hash.Length && i < second_hash.Length; i++)
                diff |= (uint)(first_hash[i] ^ second_hash[i]);
            return diff == 0;
        }

        /// <summary>
        /// Compares two hashes converting them first to byte array
        /// </summary>
        /// <param name="first_hash">first hash</param>
        /// <param name="second_hash">second hash</param>
        /// <returns>bool</returns>
        public static bool SlowEquals(string first_hash, string second_hash)
        {
            return SlowEquals(GetBytesFromString(first_hash), GetBytesFromString(second_hash));
        }

    }
}