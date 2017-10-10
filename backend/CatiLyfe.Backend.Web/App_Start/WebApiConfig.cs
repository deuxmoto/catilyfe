using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace CatiLyfe.Backend.Web
{
    using System.Web.Http.Cors;
    using System.Web.Http.ExceptionHandling;

    using CatiLyfe.Backend.Web.Code.Filters;

    using Newtonsoft.Json;
    using Newtonsoft.Json.Serialization;

    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
            //json settings
            config.Formatters.JsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            config.Formatters.JsonFormatter.SerializerSettings.NullValueHandling = NullValueHandling.Ignore;

            // Web API configuration and services
            config.Services.Replace(typeof(IExceptionHandler), new CatiLyfeExceptionFilter());

            // Enable cors.
            config.EnableCors(new EnableCorsAttribute("*", "*", "*"));

            

            // Web API routes
            config.MapHttpAttributeRoutes();

            config.SuppressHostPrincipal();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );
        }
    }
}
