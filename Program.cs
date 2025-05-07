using EmailSortClient;
using EmailSortClient.ValidationOperations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Events;

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
     services.AddHttpClient<IEmailServerHttpClient, EmailServerHttpClient>((sp, httpClient) =>
     {
         var configuration = sp.GetRequiredService<IConfiguration>();
         httpClient.BaseAddress = configuration.GetValue<Uri>("RestClient:BaseUrl");
     });

     //Later add validation-rules
     services.AddOptions<UserData>().BindConfiguration("Credentials").Services.AddTransient<UserData>(sp =>
         sp.GetRequiredService<IOptionsMonitor<UserData>>().CurrentValue
         );
     
     services.AddHostedService<Worker>();
 });
 
 var app = builder.Build();
 
 await app.RunAsync();