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
    using CatiLyfe.Common.Security;
    using CatiLyfe.DataLayer;
    using CatiLyfe.DataLayer.Sql;

    using Swashbuckle.AspNetCore.Swagger;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

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

            services.AddMvc();

            // Add the data layers.
            services.AddSingleton<ICatiDataLayer>(CatiDataLayerFactory.CreateDataLayer(constr));
            services.AddSingleton<ICatiAuthDataLayer>(CatiDataLayerFactory.CreateAuthDataLayer(constr));
            services.AddSingleton<IPasswordHelper>(new PasswordGenerator(passwordSalt));

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
