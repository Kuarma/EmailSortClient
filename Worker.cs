using EmailSortClient.Interfaces;
using Microsoft.Extensions.Hosting;

namespace EmailSortClient;

public class Worker : IHostedService
{
    private readonly IConsoleRequests _consoleRequests;
    private readonly IEmailSortClientRequests _emailSortClientRequests;
    
    public Worker(IConsoleRequests consoleRequests, IEmailSortClientRequests emailSortClientRequests)
    {
        _consoleRequests = consoleRequests;
        _emailSortClientRequests = emailSortClientRequests;
    }
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var test = await _emailSortClientRequests.LoginToEmailSortAsync(_consoleRequests.LoginToApi());
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}