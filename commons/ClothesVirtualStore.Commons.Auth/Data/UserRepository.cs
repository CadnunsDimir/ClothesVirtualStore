

namespace ClothesVirtualStore.Commons.Auth.Data;

public class UserRepository: IUserRepository
{
    private IUserDbContext dbContext;

    public UserRepository(IUserDbContext dbContext)
    {
        this.dbContext = dbContext;
    }
    public void Create(UserEntity user)
    {
        dbContext.Users.Add(user);
        dbContext.SaveOnDb();
    }

    public bool Exists(string email, string password) => 
        dbContext.Users.Any(x=> x.Email == email && x.Password == password);

    public UserEntity? Get(string email, string password) => 
        dbContext.Users.FirstOrDefault(x=> x.Email == email && x.Password == password);

    public UserEntity? Get(string email) => 
        dbContext.Users.FirstOrDefault(x=> x.Email == email);
}
