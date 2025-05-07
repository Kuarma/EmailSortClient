using System.Net.Http.Json;
using EmailSortClient.ValidationOperations;
using Serilog;

namespace EmailSortClient;

public class EmailServerHttpClient : IEmailServerHttpClient
{
    private readonly HttpClient _httpClient;
    
    public EmailServerHttpClient(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }
    
    public async Task<LoginResponse?> LoginToEmailSortAsync(UserData userData)
    {
        try
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("/login", userData);
            return await responseMessage.Content.ReadFromJsonAsync<LoginResponse>();
        }
        catch (Exception exception)
        {
            Log.Error(exception, "An error occured while logging in to email sort");
            return null;
        }
    }
    
    public async Task RegisterToEmailSortAsync(UserData userData)
    {
        try
        {
            var responseMessage = await _httpClient.PostAsJsonAsync("/register", userData);
            responseMessage.EnsureSuccessStatusCode();
        }
        catch (Exception exception)
        {
            Log.Error(exception, "An error occured while registering to email sort");
        }
    }
}