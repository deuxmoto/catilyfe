namespace CatiLyfe.Common.Exceptions
{
    public class DuplicateItemException : CatiFailureException
    {
        public DuplicateItemException(string message) : base(409, "DUPLICATE", message)
        {
            // Nothing
        }
    }
}
