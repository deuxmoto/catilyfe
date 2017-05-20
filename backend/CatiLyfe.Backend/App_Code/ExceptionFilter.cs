namespace CatiLyfe.Backend.App_Code
{
    using System;

    using CatiLyfe.Common.Exceptions;

    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Filters;
    public class ExceptionFilter : IExceptionFilter
    {
        public void OnException(ExceptionContext context)
        {
            var catiException = context.Exception as CatiFailureException;
            if (null != catiException)
            {
                context.Result = new ErrorResult(catiException.HtmlErrorCode, catiException.ShortMessage, catiException.Message);
                return;
            }

            context.Result = new ErrorResult(500, string.Empty, context.Exception.Message);
        }
    }
}
