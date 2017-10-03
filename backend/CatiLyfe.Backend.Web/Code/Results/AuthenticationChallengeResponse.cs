using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatiLyfe.Backend.Web.Code.Results
{
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http;
    public class AuthenticationChallengeResponse : IHttpActionResult
    {
        private readonly AuthenticationHeaderValue header;

        private readonly IHttpActionResult result;

        public AuthenticationChallengeResponse(AuthenticationHeaderValue challenge, IHttpActionResult innerResult)
        {
            this.header = challenge;
            this.result = innerResult;
        }

        public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
        {
            var response = await this.result.ExecuteAsync(cancellationToken);

            if (response.StatusCode == HttpStatusCode.Unauthorized)
            {
                if (false == response.Headers.WwwAuthenticate.Any(h => h.Scheme == this.header.Scheme))
                {
                    response.Headers.WwwAuthenticate.Add(this.header);
                }
            }

            return response;
        }
    }
}