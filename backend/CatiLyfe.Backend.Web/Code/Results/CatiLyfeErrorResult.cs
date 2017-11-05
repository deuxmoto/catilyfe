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

        private readonly string stacktrace;

        private readonly bool detailed;

        public CatiLyfeErrorResult(HttpStatusCode code, bool detailed, string shortMessage, string longMessage, string stackTrace)
        {
            this.code = code;
            this.detailed = detailed;
            this.shortMessage = shortMessage;
            this.longMessage = longMessage;
            this.stacktrace = stackTrace;
        }

        public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var message = new HttpResponseMessage(this.code);
            message.ReasonPhrase = this.shortMessage;
            if (this.detailed)
            {
                message.Content = new StringContent(JObject.FromObject(new
                                                                           {
                                                                               message = this.longMessage,
                                                                               stacktrace = this.stacktrace
                                                                           }).ToString());
            }
            else
            {
                message.Content = new StringContent(JObject.FromObject(new { message = this.longMessage }).ToString());
            }

            message.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return Task.FromResult(message);
        }
    }
}