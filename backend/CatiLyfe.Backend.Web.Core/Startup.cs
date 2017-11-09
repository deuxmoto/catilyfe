using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace CatiLyfe.Backend.Web.Core
{
    using CatiLyfe.Backend.Web.Core.Code;
    using CatiLyfe.Backend.Web.Core.Code.Filters;
    using CatiLyfe.Common.Security;
    using CatiLyfe.DataLayer;
    using CatiLyfe.DataLayer.Sql;

    using Microsoft.AspNetCore.Authentication.Cookies;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Server.IISIntegration;

    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// The configuration.
        /// </summary>
        public IConfiguration Configuration { get; }

        /// <summary>
        /// Configuration for services.
        /// </summary>
        /// <param name="services">The services collection.</param>
        public void ConfigureServices(IServiceCollection services)
        {
            var constr = this.Configuration.GetConnectionString("default");
            var security = this.Configuration.GetSection("Security");

            var passwordSalt = security["salt"];
            var authDataLayer = CatiDataLayerFactory.CreateAuthDataLayer(constr);

            // Add the data layers.
            services.AddSingleton<ICatiDataLayer>(CatiDataLayerFactory.CreateDataLayer(constr));
            services.AddSingleton<ICatiAuthDataLayer>(authDataLayer);
            services.AddSingleton<IPasswordHelper>(new PasswordGenerator(passwordSalt));
            services.AddSingleton<IContentTransformer>(new MarkdownProcessor());
            services.AddSingleton<IAuthorizationHandler, DefaultAuthorizationHandler>().AddAuthorization(
                options =>
                    {
                        options.AddPolicy("default", policy => policy.Requirements.Add(new DefaultAuthorizationRequirement()));
                    });

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(
                config =>
                    {
                        config.Cookie.Name = "CatiCookie";
                        config.Events.OnRedirectToLogin = options =>
                            {
                                options.Response.StatusCode = 401;
                                return Task.CompletedTask;
                            };
                        config.Events.OnRedirectToAccessDenied = options =>
                            {
                                options.Response.StatusCode = 401;
                                return Task.CompletedTask;
                            };
                        config.Events.OnRedirectToReturnUrl = options =>
                        {
                            options.Response.StatusCode = 401;
                            return Task.CompletedTask;
                        };
                    });

            services.AddCors(
                options =>
                    {
                        options.AddPolicy(
                            "default",
                            policy =>
                                {
                                    policy.AllowAnyHeader();
                                    policy.AllowAnyMethod();
                                    policy.AllowAnyOrigin();
                                });
                    });

            services.AddMvc(
                config =>
                    {
                        // config.Filters.Add(new AuthorizationFilter(authDataLayer));
                        config.Filters.Add(new CatiExceptionFilter());
                    });

            // Add the documentation
            services.AddSwaggerGen(
                config =>
                    {
                        config.SwaggerDoc("v1", new Info { Title = "Cati Lyfe Api", Version = "0.0.0.0.0.0.1" });
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseAuthentication();
            app.UseCors("default");

            // Enable api documentation
            app.UseSwagger();
            app.UseSwaggerUI(
                config =>
                    {
                        config.SwaggerEndpoint("/swagger/v1/swagger.json", "Cati Lyfe Api");
                    });

            app.UseMvc();
        }
    }
}

