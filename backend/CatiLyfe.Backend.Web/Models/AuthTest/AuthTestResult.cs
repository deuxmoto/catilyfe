namespace CatiLyfe.Backend.Web.Models.AuthTest
{
    /// <summary>
    /// The auth test result.
    /// </summary>
    public class AuthTestResult
    {
        /// <summary>
        /// Initializes a new AuthTestResult.
        /// </summary>
        /// <param name="message">The message.</param>
        public AuthTestResult(string message)
        {
            this.Message = message;
        }

        /// <summary>
        /// Gets the message.
        /// </summary>
        public string Message { get; }
    }
}