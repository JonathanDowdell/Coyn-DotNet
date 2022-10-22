namespace Coyn.User.Model;

public class SignUpUserRequest
{

    public SignUpUserRequest(string email, string password)
    {
        this.Email = email;
        this.Password = password;
    }
    
    public string Email { get; set; }
    public string Password { get; set; }
}