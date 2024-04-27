using Serilog;
using Serilog.Events;
using Serilog.Exceptions;
using Serilog.Filters;
using Atividade02.Proposals.API.Middlewares;

namespace Atividade02.Proposals.API.Configurations.Serilog
{
    public static class SerilogExtension
    {
        public static IHostBuilder AddLogs(this IHostBuilder builder, IConfiguration configuration, string applicationName)
        {
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .Enrich.WithProperty("ApplicationName", $"{applicationName} - {Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}")
                .Enrich.WithCorrelationId()
                .Enrich.WithExceptionDetails()
                .Filter.ByExcluding(Matching.FromSource("Microsoft.AspNetCore.StaticFiles"))
                //.WriteTo.Async(writeTo => writeTo.Elasticsearch(new ElasticsearchSinkOptions(new Uri(configuration["HorusConfiguration:ElasticsearchSettings:Uri"]))
                //{
                //    TypeName = null,
                //    AutoRegisterTemplate = true,
                //    IndexFormat = configuration["HorusConfiguration:ElasticsearchSettings:Index"],
                //    BatchAction = ElasticOpType.Create,
                //    CustomFormatter = new EcsTextFormatter(),
                //    ModifyConnectionSettings = x => x.BasicAuthentication(configuration["HorusConfiguration:ElasticsearchSettings:Username"], configuration["HorusConfiguration:ElasticsearchSettings:Password"])
                //}))
                .WriteTo.Async(writeTo => writeTo.Console(outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss.fff}] [{Level}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
                .CreateLogger();

            builder.ConfigureLogging(c => c.ClearProviders());
            builder.UseSerilog(Log.Logger, true);

            return builder;
        }

        public static IApplicationBuilder UseLogs(this IApplicationBuilder app, IConfiguration configuration)
        {

            app.UseMiddleware<ExceptionMiddleware>();
            app.UseSerilogRequestLogging(opt =>
            {
                opt.EnrichDiagnosticContext = EnricherLog.EnrichFromRequest;
            });

            

            return app;
        }
    }
}

