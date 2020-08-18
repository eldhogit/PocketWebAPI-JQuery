using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace Pocket.WebAPI
{
    public static class WebApiConfig
    {
        public static void Register(HttpConfiguration config)
        {
         

            config.MapHttpAttributeRoutes();

            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Formatters.XmlFormatter.SupportedMediaTypes.Clear();

            var json = config.Formatters.JsonFormatter.SerializerSettings;

            json.Formatting = Newtonsoft.Json.Formatting.Indented;

            //config.Formatters.Remove(config.Formatters.XmlFormatter);

        }
    }
}
