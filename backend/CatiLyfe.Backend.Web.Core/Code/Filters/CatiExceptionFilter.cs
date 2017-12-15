namespace CatiLyfe.Backend.Web.Core.Code.Filters
{
    using System.Threading.Tasks;

    using CatiLyfe.Common.Exceptions;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;

    /// <summary>
    /// The cati exception filter.
    /// </summary>
    public class CatiExceptionFilter : IAsyncExceptionFilter
    {
        /// <summary>
        /// The exception handler.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns>An async task.</returns>
        public Task OnExceptionAsync(ExceptionContext context)
        {
            var catiException = context.Exception as CatiFailureException ?? new DeveloperIsAnIdiotException(context.Exception);

            var result = new JsonResult(new { Message = catiException.ShortMessage, Details = catiException.Value, StackTrace = context.Exception.StackTrace })
                             {
                                 StatusCode =
                                     catiException
                                         .HtmlErrorCode
                             };
            context.Result = result;
            context.ExceptionHandled = true;
            return Task.FromResult(0);
        }
    }
}
