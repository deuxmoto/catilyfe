using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CatiLyfe.Backend.Web.App_Start
{
    using System.Configuration;
    using System.Threading.Tasks;

    using Microsoft.IdentityModel.Tokens;
    using Microsoft.Owin.Security;
    using Microsoft.Owin.Security.ActiveDirectory;
    using Microsoft.Owin.Security.OpenIdConnect;

    using Owin;

    public partial class Startup
    {
        public static string clientId = ConfigurationManager.AppSettings["ida:ClientId"];
        public static string aadInstance = ConfigurationManager.AppSettings["ida:AADInstance"];
        private static string redirectUri = ConfigurationManager.AppSettings["ida:RedirectUri"];
        private static string aadTenant = ConfigurationManager.AppSettings["ida:Tenant"];

        private static string audience = ConfigurationManager.AppSettings["ida:Audience"];

        public void ConfigureAuth(IAppBuilder app)
        {
            app.UseWindowsAzureActiveDirectoryBearerAuthentication(
                new WindowsAzureActiveDirectoryBearerAuthenticationOptions()
                    {
                        TokenValidationParameters = new TokenValidationParameters()
                                       {
                                           ValidAudience = audience
                                       },
                        Tenant = aadTenant
                    });
        }
    }
}