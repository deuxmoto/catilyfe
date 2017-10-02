using System;

namespace CatiLyfe.Backend.Web.Code.Filters
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;

    using Newtonsoft.Json.Linq;

    public class CatiLyfeErrorResult : IHttpActionResult
    {
        private readonly HttpStatusCode code;

        private readonly string shortMessage;

        private readonly string longMessage;

        public CatiLyfeErrorResult(HttpStatusCode code, string shortMessage, string longMessage)
        {
            this.code = code;
            this.shortMessage = shortMessage;
            this.longMessage = longMessage;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var message = new HttpResponseMessage(this.code);
            message.ReasonPhrase = this.shortMessage;
            message.Content = new StringContent(JObject.FromObject(new { message = this.longMessage }).ToString());
            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return Task.FromResult(message);
        }
    }
}