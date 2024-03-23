

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

    public bool Exists(string email, string password) => dbContext.Users.Any(x=> x.Email == email && x.Password == password);

    public UserEntity? Get(string email, string password)
    {
        // var db = new User[] {
        //     new User("calangoSpider", "calango@spider.com" , CustomRoles.Admin, "123456")
        // };

        // return db.FirstOrDefault(x=> x.Email == email && x.Password == password);
        return dbContext.Users.FirstOrDefault(x=> x.Email == email && x.Password == password);
    }

    public UserEntity? Get(string email)
    {
        return dbContext.Users.FirstOrDefault(x=> x.Email == email);
    }
}
