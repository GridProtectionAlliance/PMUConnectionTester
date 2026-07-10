using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ConnectionTester.Api;

/// <summary>
/// OWIN startup - wires Web API routing, JSON formatting and Swagger/Swagger UI.
/// </summary>
public class Startup
{
    public void Configuration(IAppBuilder app)
    {
        HttpConfiguration config = new();

        config.MapHttpAttributeRoutes();

        config.Formatters.Remove(config.Formatters.XmlFormatter);

        JsonMediaTypeFormatter jsonFormatter = config.Formatters.JsonFormatter;
        jsonFormatter.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        jsonFormatter.SerializerSettings.Formatting = Newtonsoft.Json.Formatting.Indented;

        config.EnableSwagger(c => c.SingleApiVersion("v1", "PMU Connection Tester API"))
              .EnableSwaggerUi();

        app.UseWebApi(config);
    }
}