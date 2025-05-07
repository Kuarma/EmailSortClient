using EmailSortClient.ValidationOperations;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace EmailSortClient;

public class Worker : BackgroundService
{
    private readonly UserData _userData;
    private readonly ILogger<Worker> _logger;
    private readonly IEmailServerHttpClient _emailSortClientRequests;
    
    public Worker(UserData userData, ILogger<Worker> logger, IEmailServerHttpClient emailServerHttpClient)
    {
        _userData = userData;
        _logger = logger;
        _emailSortClientRequests = emailServerHttpClient;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var userTest = new UserData
        {
            Email = "furkyuaudbi@gmaail.com",
            Password = "thisIsATestPw!1"
        };
        var test = await _emailSortClientRequests.LoginToEmailSortAsync(userTest);
        Console.WriteLine(test);
    }
}