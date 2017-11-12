namespace CatiLyfe.Common.Exceptions
{
    /// <summary>
    /// The auth failure exception.
    /// </summary>
    public class AuthFailureException : CatiFailureException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AuthFailureException"/> class.
        /// </summary>
        public AuthFailureException()
            : base(400, "CATI AUTH BAD", "Authorization has failed.")
        {
        }
    }
}
