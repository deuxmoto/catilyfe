namespace CatiLyfe.Common.Exceptions
{
    public class RevisionMismatchException : CatiFailureException
    {
        public RevisionMismatchException(string message) : base(409, "CONFLICT", message)
        {
            // nothing
        }
    }
}
