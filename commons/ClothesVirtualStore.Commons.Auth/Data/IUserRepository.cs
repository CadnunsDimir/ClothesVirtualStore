namespace ClothesVirtualStore.Commons.Auth.Data;

public interface IUserRepository
{
    UserEntity? Get(string email);

    bool Exists(string email, string password);

    void Create(UserEntity user);
}