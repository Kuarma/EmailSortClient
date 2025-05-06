using EmailSortClient.ConsoleOperations;
using EmailSortClient.Interfaces;
using RestSharp;
using Serilog;

namespace EmailSortClient.AuthenticateToServer;

public class EmailSortServerApiRequests : IEmailSortClientRequests
{
    private readonly RestClient _client;
    
    public EmailSortServerApiRequests(RestClient client)
    {
        _client = client;
    }

    public async Task<RestResponse?> LoginToEmailSortAsync(ApiLoginRecord loginRecord)
    {
        try
        {
            var request = new RestRequest("/login", Method.Post)
                .AddParameter("email", loginRecord.Email, false)
                .AddParameter("password", loginRecord.Password, false);
            return await _client.PostAsync(request);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "An error occured while logging in to email sort");
            return null;
        }
    }

    public async Task<RestResponse?> RegisterToEmailSortAsync(string email, string password)
    {
        try
        {
            var request = new RestRequest("/register", Method.Post)
                .AddParameter("email", email)
                .AddParameter("password", password);
            return await _client.PostAsync(request);
        }
        catch (Exception exception)
        {
            Log.Error(exception, "An error occured while registering to email sort");
            return null;
        }
    }
}