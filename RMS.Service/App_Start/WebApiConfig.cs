using RMS.Service.Filters;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.ExceptionHandling;

namespace RMS.Service
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {


            // Web API configuration and services
            var cors = new EnableCorsAttribute("*", "*", "*");
            config.EnableCors(cors);

            //Filters
            //To vaildate modal state
            config.Filters.Add(new ValidateModelAttribute());
            //To authenticate user
            config.Filters.Add(new AuthenticateUser());


            // Web API routes
            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{action}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            //Services
            config.Services.Add(typeof(IExceptionLogger), new RMSExceptionLogger());
        }
    }
}
