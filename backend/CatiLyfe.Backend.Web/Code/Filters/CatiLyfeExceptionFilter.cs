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
            var detailed = context.Request.IsLocal();

            var exception = context.Exception as CatiFailureException;
            if (exception != null)
            {
                context.Result = new CatiLyfeErrorResult((HttpStatusCode)exception.HtmlErrorCode, detailed, "CATI ERROR", exception.Message, exception.StackTrace);
                return Task.FromResult(1);
            }

            var message = detailed ? context.Exception.Message : "An error has occurred.";
            context.Result = new CatiLyfeErrorResult(HttpStatusCode.InternalServerError, detailed, "CATI ERROR", message, context.Exception.StackTrace);
            return Task.FromResult(1);
        }
    }
}