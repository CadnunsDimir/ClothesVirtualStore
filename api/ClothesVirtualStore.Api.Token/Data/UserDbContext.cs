using ClothesVirtualStore.Commons.Auth;
using ClothesVirtualStore.Commons.Auth.Data;
using Microsoft.EntityFrameworkCore;

namespace ClothesVirtualStore.Api.Token.Data;
public class UserDbContext(IConfiguration configuration) : DbContext, IUserDbContext
{
    private readonly string connectionString = configuration.GetConnectionString("MySql") ?? throw new ArgumentNullException("ConnectionString.MySql");
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseMySQL(connectionString);
    public DbSet<UserEntity> Users {get; private set; }

    public void SaveOnDb() => base.SaveChanges();

    public UserEntity CreateAdminUser()
    {
        var adminUser = new UserEntity(Guid.NewGuid(), "admin",  "admin@cvs.cadnunsdmir.github.io","84780245060", CustomRoles.Admin.ToString(),Guid.NewGuid().ToString());
        Users.Add(adminUser);
        SaveOnDb();
        return adminUser;
    }
}