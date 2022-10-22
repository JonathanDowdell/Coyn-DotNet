using Coyn.Token.Data;
using Coyn.User.Data;
using Microsoft.EntityFrameworkCore;

namespace Coyn.Databases;

public class ApplicationApiDbContext: DbContext
{
    public ApplicationApiDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<UserEntity> Users { get; set; }
    public DbSet<RefreshTokenEntity> RefreshTokens { get; set; }
}