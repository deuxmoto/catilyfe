using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatiLyfe.Backend.Web.Code.Results
{
    using System.Net;
    using System.Net.Http;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    public class AuthenticationFailureResult : IHttpActionResult
    {
        private readonly string message;

        private readonly HttpRequestMessage request; 

        public AuthenticationFailureResult(string message, HttpRequestMessage request)
        {
            this.message = message;
            this.request = request;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.RequestMessage = this.request;
            response.ReasonPhrase = this.message;
            return Task.FromResult(response);
        }
    }
}