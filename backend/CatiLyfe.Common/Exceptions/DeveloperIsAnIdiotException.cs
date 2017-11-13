namespace CatiLyfe.Common.Exceptions
{
    using System;

    public class DeveloperIsAnIdiotException : CatiFailureException
    {
        public DeveloperIsAnIdiotException(Exception source = null)
            : base(500, "DEV IS IDIOT", source?.Message ?? "Our developer has done something stupid. Down' worry. He will be notified.")
        {
        }
    }
}
