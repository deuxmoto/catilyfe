namespace CatiLyfe.Common.Exceptions
{
    public class InvalidArgumentException : CatiFailureException
    {
        public InvalidArgumentException(string longMesssage)
            : base(400, "BAD REQUESt", longMesssage)
        {
        }
    }
}
