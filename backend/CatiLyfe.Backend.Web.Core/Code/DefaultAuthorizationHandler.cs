using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatiLyfe.Backend.Web.Core.Code
{
    using System.Security.Claims;

    using CatiLyfe.DataLayer;

    using Microsoft.AspNetCore.Authorization;
    public class DefaultAuthorizationHandler : AuthorizationHandler<DefaultAuthorizationRequirement>
    {
        private readonly ICatiAuthDataLayer authDataLayer;

        public DefaultAuthorizationHandler(ICatiAuthDataLayer authdataLayer)
        {
            this.authDataLayer = authdataLayer;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, DefaultAuthorizationRequirement requirement)
        {
            if (false == context.User.Identity.IsAuthenticated
                || false == (context.User.Identity.AuthenticationType == "catilyfe"))
            {
                context.Fail();
                return;
            }

            var token = context.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Hash);

            if (token == null)
            {
                context.Fail();
                return;
            }

            try
            {
                var user = (await this.authDataLayer.GetUser(null, null, Convert.FromBase64String(token.Value))).FirstOrDefault();

                if (user == null)
                {
                    context.Fail();
                    return;
                }

                var claims = new[]
                             {
                                 new Claim(ClaimTypes.Name, user.Name), new Claim(ClaimTypes.Email, user.Email),
                                 new Claim(ClaimTypes.AuthenticationMethod, "catilyfe"),
                                 new Claim(ClaimTypes.AuthenticationInstant, DateTime.UtcNow.ToLongTimeString()),
                                 new Claim(ClaimTypes.Authentication, "yes"),
                                 new Claim(ClaimTypes.Sid, user.Id.ToString())
                             };

                var claimsIdentity = (ClaimsIdentity)context.User.Identity;
                claimsIdentity.AddClaims(user.Roles.Select(r => new Claim(ClaimTypes.Role, r)).Concat(claims.Where(c => !context.User.HasClaim(ec => ec.Type == c.Type))));

                context.Succeed(requirement);
            }
            catch
            {
                // Nuffin
                context.Fail();
            }
        }
    }
}
