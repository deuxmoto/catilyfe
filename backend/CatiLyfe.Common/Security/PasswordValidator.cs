namespace CatiLyfe.Common.Security
{
    using CatiLyfe.Common.Exceptions;

    /// <summary>
    /// The password validator.
    /// </summary>
    public static class PasswordValidator
    {
        /// <summary>
        /// test two passwords for equality.
        /// </summary>
        /// <param name="actualPassword">The actual password.</param>
        /// <param name="testPassword">The test password.</param>
        /// <exception cref="AuthFailureException">On failure.
        /// </exception>
        public static void Validate(byte[] actualPassword, byte[] testPassword)
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
