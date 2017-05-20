using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatiLyfe.Backend.App_Code
{
    using CatiLyfe.Common.Exceptions;

    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    public class ErrorResult : IActionResult
    {
        public readonly int statusCode;

        public readonly string shortMessage;

        public readonly string longMessage;

        public ErrorResult(int statusCode, string shortMesssage, string longMessage)
        {
            this.statusCode = statusCode;
            this.shortMessage = shortMesssage;
            this.longMessage = longMessage;
        }

        public Task ExecuteResultAsync(ActionContext context)
        {
            context.HttpContext.Response.StatusCode = this.statusCode;
            context.HttpContext.Response.ContentType = "text/plain";

            return context.HttpContext.Response.WriteAsync(this.longMessage);
        }
    }
}
