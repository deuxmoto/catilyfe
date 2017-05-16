using System;

namespace CatiLyfe.Common.Exceptions
{
    public abstract class CatiFailureException : Exception
    {
        public CatiFailureException(int htmlErroCode, string shortMessage, string longMesssage) : base(message: longMesssage)
        {
            this.HtmlErrorCode = htmlErroCode;
            this.ShortMessage = shortMessage;
        }

        /// <summary>
        /// Gets the html error code.
        /// </summary>
        public int HtmlErrorCode { get; }

        /// <summary>
        /// Gets the short error message for http.
        /// </summary>
        public string ShortMessage { get; }
    }
}
