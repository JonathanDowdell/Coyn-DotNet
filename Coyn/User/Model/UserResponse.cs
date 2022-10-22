using Coyn.User.Data;

namespace Coyn.User.Model;

public class UserResponse
{
    public UserResponse(UserEntity userEntity)
    {
        this.Email = userEntity.Email;
        this.CreationDate = userEntity.CreationDate.ToString("MM/dd/yyyy HH:mm:ss");
    }
    
    public UserResponse(string email, string creationDate)
    {
        Email = email;
        CreationDate = creationDate;
    }

    public string Email { get; set; }
    public string CreationDate { get; set; }
 
}