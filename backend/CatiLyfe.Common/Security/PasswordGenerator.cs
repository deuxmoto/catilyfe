namespace CatiLyfe.Common.Security
{
    using System;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;

    using CatiLyfe.Common.Exceptions;

    /// <summary>
    /// The password generator.
    /// </summary>
    public class PasswordGenerator : IPasswordHelper
    {
        /// <summary>
        /// The salt bytes.
        /// </summary>
        private readonly byte[] saltBytes;

        /// <summary>
        /// Initializes a new instance of the <see cref="PasswordGenerator"/> class.
        /// </summary>
        /// <param name="passwordSalt">The password salt.</param>
        public PasswordGenerator(string passwordSalt)
        {
           this.saltBytes = Encoding.Unicode.GetBytes(passwordSalt);
        }

        /// <summary>
        /// Hashes a password.
        /// </summary>
        /// <param name="password">The password.</param>
        /// <returns>The hashed password.</returns>
        public byte[] HashPassword(string password)
        {
            var passwordBytes = Encoding.Unicode.GetBytes(password);

            var binary = this.saltBytes.Concat(passwordBytes).ToArray();

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
        public byte[] GenerateTokenBytes(int length)
        {
            var bytes = new byte[length];
            using (var random = RandomNumberGenerator.Create())
            {
                random.GetNonZeroBytes(bytes);
            }

            return bytes;
        }

        /// <summary>
        /// test two passwords for equality.
        /// </summary>
        /// <param name="actualPassword">The actual password.</param>
        /// <param name="testPassword">The test password.</param>
        /// <exception cref="AuthFailureException">On failure.
        /// </exception>
        public void Validate(byte[] actualPassword, byte[] testPassword)
        {
            if (actualPassword.Length != testPassword.Length)
            {
                throw new AuthFailureException();
            }

            for (var i = 0; i < actualPassword.Length; i++)
            {
                if (actualPassword[i] != testPassword[i])
                {
                    throw new AuthFailureException();
                }
            }
        }
    }
}
