

using ClothesVirtualStore.Commons.Auth;

namespace ClothesVirtualStore.Api.Token;

public class UserRepository
{
    public User? Get(string email, string password)
    {
        var db = new User[] {
            new User("calangoSpider", "calango@spider.com" , "Admin", "123456")
        };

        return db.FirstOrDefault(x=> x.Email == email && x.Password == password);
    }
}
