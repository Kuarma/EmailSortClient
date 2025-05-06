using EmailSortClient.ConsoleOperations;
using RestSharp;

namespace EmailSortClient.Interfaces;

public interface IEmailSortClientRequests
{
    public Task<RestResponse?> LoginToEmailSortAsync(ApiLoginRecord loginRecord);
    
    public Task<RestResponse?> RegisterToEmailSortAsync(string email, string password);
}