namespace CatiLyfe.Backend
{
    using System.IO;

    using CatiLyfe.Backend.App_Code;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.PlatformAbstractions;

    using Swashbuckle.Swagger.Model;

    /// <summary>
    /// The startup.
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class.
        /// </summary>
        /// <param name="environment">
        /// The environment.
        /// </param>
        public Startup(IHostingEnvironment environment)
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(environment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .AddJsonFile($"appsettings.{environment.EnvironmentName}.json", optional: true)
                .AddEnvironmentVariables();
            this.Configuration = builder.Build();

            CatiData.InitializeDataLayer(this.Configuration.GetConnectionString("catilyfe"));
        }

        public IConfigurationRoot Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            // Add framework services.
            var mvcBuilder = services.AddMvc();
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

            mvcBuilder.AddMvcOptions(options => options.Filters.Add(new ExceptionFilter()));
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Logging"));
            loggerFactory.AddDebug();


            // Must live here.
            app.UseCors(builder => builder.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().AllowCredentials());

            app.UseMvc();

            app.UseSwagger();
            app.UseSwaggerUi(baseRoute: "docs");
            app.UseDeveloperExceptionPage();
        }

        private string GetDocumentationPath()
        {
            var app = PlatformServices.Default.Application;
            return Path.Combine(app.ApplicationBasePath, "CatiLyfe.Backend.xml");
        }
    }
}
