using Coyn.User.Data;

namespace Coyn.Token.Data;

public class RefreshTokenEntity
{
    public Guid Id { get; set; }
    
    public DateTime Created { get; set; } = DateTime.Now;

    public UserEntity User { get; set; }
}