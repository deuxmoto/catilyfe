namespace CatiLyfe.Common.Security
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    /// <summary>
    /// The password generator.
    /// </summary>
    public static class PasswordGenerator
    {
        /// <summary>
        /// Hashes a password.
        /// </summary>
        /// <param name="salt">The password salt in base 64.</param>
        /// <param name="password">The password.</param>
        /// <returns>The hashed password.</returns>
        public static byte[] HashPassword(string salt, string password)
        {
            var saltBytes = Encoding.Unicode.GetBytes(salt);
            var passwordBytes = Encoding.Unicode.GetBytes(password);

            var binary = saltBytes.Concat(passwordBytes).ToArray();

            using (var hash = SHA512Managed.Create())
            {
                return hash.ComputeHash(binary);
            }
        }

        /// <summary>
        /// Generate random bytes for a token.
        /// </summary>
        /// <param name="length">The length in bytes.</param>
        /// <returns>The random bytes.</returns>
        public static byte[] GenerateTokenBytes(int length)
        {
            var bytes = new byte[length];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetNonZeroBytes(bytes);
            }

            return bytes;
        }
    }
}
