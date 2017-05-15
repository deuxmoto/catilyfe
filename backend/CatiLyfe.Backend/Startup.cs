using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Swashbuckle.Swagger.Application;
using Swashbuckle.SwaggerGen.Application;
using Swashbuckle.Swagger;
using Swashbuckle.SwaggerGen;
using Swashbuckle.Swagger.Model;

namespace CatiLyfe.Backend
{
    using Microsoft.Extensions.PlatformAbstractions;
    using System.IO;

    public class Startup
    {
        public Startup(IHostingEnvironment env)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            Configuration = builder.Build();
        }

        public IConfigurationRoot Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            services.AddMvc();
            services.AddSwaggerGen();
            services.ConfigureSwaggerGen(
                options =>
                    {
                        options.SingleApiVersion(new Info
                                                     {
                                                         Version = "v1",
                                                         Title = "Cati Lyfe",
                                                         Description = "SHOOOT",
                                                         TermsOfService = "Copywrite forever",
                                                         Contact = new Contact { Name = "Cati Lyfe"}
                                                     });
                        //options.IncludeXmlComments(this.GetDocumentationPath());
                    });
            services.AddCors(
                options =>
                    {
                        options.AddPolicy(
                            "AllowCatiLyfe",
                            builder => { builder.WithOrigins("https://catilyfe.azurewebsites.net"); });
                    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();

            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUi(baseRoute: "docs");
            app.UseDeveloperExceptionPage();

            app.UseCors("AllowCatiLyfe");
        }

        private string GetDocumentationPath()
        {
            var app = PlatformServices.Default.Application;
            return Path.Combine(app.ApplicationBasePath, "CatiLyfe.Backend.xml");
        }
    }
}
