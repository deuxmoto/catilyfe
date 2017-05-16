namespace CatiLyfe.Common.Exceptions
{
    public class DeveloperIsAnIdiotException : CatiFailureException
    {
        public DeveloperIsAnIdiotException()
            : base(500, "DEV IS IDIOT", "Our developer has done something stupid. Down' worry. He will be notified.")
        {
        }
    }
}
