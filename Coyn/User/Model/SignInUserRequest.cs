namespace Coyn.User.Model;

public class SignInUserRequest
{
    public SignInUserRequest(string email, string password)
    {
        Email = email;
        Password = password;
    }

    public string Email { get; set; }
    public string Password { get; set; } 
}