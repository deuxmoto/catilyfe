using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CatiLyfe.Backend.Web.Core.Code
{
    using Microsoft.AspNetCore.Authorization;
    public class DefaultAuthorizationHandler : AuthorizationHandler<DefaultAuthorizationRequirement>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, DefaultAuthorizationRequirement requirement)
        {
            if (context.User.Identity.IsAuthenticated && context.User.Identity.AuthenticationType == "catilyfe")
            {
                context.Succeed(requirement);
            }

            return Task.FromResult(0);
        }
    }
}
