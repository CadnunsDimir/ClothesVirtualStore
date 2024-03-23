using ClothesVirtualStore.Commons.Auth;
using ClothesVirtualStore.Commons.Auth.Data;

public class AuthUseCases
{
    private IUserRepository userRepository;

    public AuthUseCases(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public void CreateNewUser(NewUser user){
        var entity = user.ToEntity();
        if(entity.IsValid()) {
            userRepository.Create(entity);
        }
    }

    public User? GetByCredentials(string email, string password) {
        return !userRepository.Exists(email, password) ? null : User.Map(userRepository.Get(email));
    }

    public void CreateCustomer(CustomerUser customer){
        var entity = customer.ToEntity();
        if(entity.IsValid()) {
            userRepository.Create(entity);
        }
    }
}