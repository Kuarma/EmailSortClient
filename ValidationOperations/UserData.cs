namespace EmailSortClient.ValidationOperations;

public record UserData
{
    public string Email { get; set; }
    public string Password { get; set; }
}