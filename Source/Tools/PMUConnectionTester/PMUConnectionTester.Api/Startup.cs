using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using Owin;
using Swashbuckle.Application;
using System;
using System.IO;
using System.Net.Http.Formatting;
using System.Reflection;
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
        jsonFormatter.SerializerSettings.Converters.Add(new StringEnumConverter());

        config.EnableSwagger(c =>
        {
            c.SingleApiVersion("v1", "PMU Connection Tester API")
             .Description("REST API for running PMU/PDC connectivity tests (file playback, TCP and UDP) without the desktop application.")
             .Contact(cc => cc
                 .Name("Grid Protection Alliance")
                 .Url("https://www.gridprotectionalliance.org/"))
             .License(lc => lc
                 .Name("MIT")
                 .Url("https://github.com/GridProtectionAlliance/PMUConnectionTester/blob/master/LICENSE"));

            string xmlCommentsPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, $"{Assembly.GetExecutingAssembly().GetName().Name}.xml");

            if (File.Exists(xmlCommentsPath))
                c.IncludeXmlComments(xmlCommentsPath);

            c.DescribeAllEnumsAsStrings();
        })
        .EnableSwaggerUi();

        app.UseWebApi(config);
    }
}