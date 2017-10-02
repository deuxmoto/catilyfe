namespace CatiLyfe.Backend.Web.Code.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.ExceptionHandling;
    using System.Web.Http.Filters;
    using System.Web.Mvc;

    using CatiLyfe.Common.Exceptions;

    /// <summary>
    /// The exception filter.
    /// </summary>
    public class CatiLyfeExceptionFilter : IExceptionHandler
    {
        public Task HandleAsync(ExceptionHandlerContext context, CancellationToken cancellationToken)
        {
            var exception = context.Exception as CatiFailureException;
            if (exception != null)
            {
                context.Result = new CatiLyfeErrorResult((HttpStatusCode)exception.HtmlErrorCode, "CATI ERROR", "AN ERROR HAS OCCURRED");
                return Task.FromResult(1);
            }

            context.Result = new CatiLyfeErrorResult(HttpStatusCode.InternalServerError, "CATI ERROR", "Error has occurred.");
            return Task.FromResult(1);
        }
    }
}