namespace CatiLyfe.Backend.Web.Code.Filters
{
    using System;
    using System.Net.Cache;
    using System.Net.Http.Headers;
    using System.Security.Principal;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Web.Http.Filters;

    using CatiLyfe.Backend.Web.Code.Results;

    public class AuthenticateAttribute : Attribute, IAuthenticationFilter
    {
        public bool AllowMultiple { get => false; }

        public async Task AuthenticateAsync(HttpAuthenticationContext context, CancellationToken cancellationToken)
        {
            var authorizationHeader = context.Request.Headers.Authorization;

            if (authorizationHeader == null)
            {
                context.ErrorResult = new AuthenticationFailureResult("MISSIN AUTH", context.Request);
                return;
            }

            if (authorizationHeader.Scheme != "Bearer")
            {
                context.ErrorResult = new AuthenticationFailureResult("BEARER YO", context.Request);
                return;
            }

            if (string.IsNullOrWhiteSpace(authorizationHeader.Parameter))
            {
                context.ErrorResult = new AuthenticationFailureResult("NO CATI CRED", context.Request);
                return;
            }

            var principal = new GenericPrincipal(new GenericIdentity("admin"), new[] { "admin" });
            context.Principal = principal;
        }

        public Task ChallengeAsync(HttpAuthenticationChallengeContext context, CancellationToken cancellationToken)
        {
            var challenge = new AuthenticationHeaderValue("Bearer");
            context.Result = new AuthenticationChallengeResponse(challenge, context.Result);
            return Task.FromResult(0);
        }
    }
}