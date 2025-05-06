using EmailSortClient.Interfaces;

namespace EmailSortClient.ConsoleOperations;

public class ConsoleRequests : IConsoleRequests
{
    public ApiLoginRecord LoginToApi()
    {
        while (true)
        {
            Console.WriteLine("Please enter your email: ");
            var email = Console.ReadLine();
            if (string.IsNullOrEmpty(email))
            {
                continue;
            }
        
            Console.WriteLine("Please enter your password: ");
            var password = Console.ReadLine();
            if (string.IsNullOrEmpty(password))
            {
                continue;
            }
    
            Console.WriteLine("Remember me? y/n");
            var rememberMe = Console.ReadLine() == "y";
            
            return new ApiLoginRecord(email, password, rememberMe);
        }
    }
}