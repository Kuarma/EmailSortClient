using EmailSortClient;
using EmailSortClient.AuthenticateToServer;
using EmailSortClient.ConsoleOperations;
using EmailSortClient.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RestSharp;
using Serilog;
using Serilog.Events;

try
{
    var builder = Host.CreateDefaultBuilder(args);
    
    // Create Serilog logger
     Log.Logger = new LoggerConfiguration()
         .MinimumLevel.Verbose()
         .WriteTo.Console(
             restrictedToMinimumLevel: LogEventLevel.Debug
             )
         .WriteTo.File("logs.log", 
             rollingInterval: RollingInterval.Hour, 
             rollOnFileSizeLimit: true
             )
         .CreateLogger();

     builder.UseSerilog();

     builder.ConfigureServices((_, services) =>
     {
         services.AddSingleton<RestClient>(sp =>
         {
             var configuration = sp.GetRequiredService<IConfiguration>();
             return new RestClient(configuration["RestClient:BaseUrl"]!);
         });

         services.AddSingleton<IEmailSortClientRequests, EmailSortServerApiRequests>();
         services.AddSingleton<IConsoleRequests, ConsoleRequests>();
         
         services.AddHostedService<Worker>();
     });
     
     var app = builder.Build();
     
     await app.RunAsync();
}
catch (Exception ex)
{
    Log.Error(ex, "An unhandled exception has occurred.");
}
finally
{
    Log.CloseAndFlush();
}