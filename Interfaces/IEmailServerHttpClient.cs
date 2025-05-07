using EmailSortClient.ValidationOperations;

namespace EmailSortClient;

public interface IEmailServerHttpClient
{
    Task<LoginResponse?> LoginToEmailSortAsync(UserData userData);
    Task RegisterToEmailSortAsync(UserData userData);
}