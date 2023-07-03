using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Common.HelperLog;
public static class LoggerSerilogELK
{
    ///I used of this is post(https://dev.to/moe23/net-6-webapi-intro-to-elasticsearch-kibana-step-by-step-p9l) ,customized based on my own code
    public static void ConfigureLogging(IConfiguration configuration)
    {

        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        Log.Logger = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithMachineName()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
            .Enrich.WithProperty("Environment", environment)
            .ReadFrom.Configuration(configuration)
            .CreateLogger();
    }
    private static ElasticsearchSinkOptions ConfigureElasticSink(IConfiguration configuration, string environment)
    {
        var _IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}";

        var Url = configuration["ElasticConfiguration:Uri"];
        return new ElasticsearchSinkOptions(new Uri(Url))
        {
            AutoRegisterTemplate = true,
            IndexFormat = _IndexFormat
        };
    }
}

