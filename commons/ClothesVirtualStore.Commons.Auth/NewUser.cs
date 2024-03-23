namespace ClothesVirtualStore.Commons.Auth; 

public class NewUser : User
{
    public required string Password { get; set; }
    public  UserEntity ToEntity() => new (Guid.NewGuid(), UserName, Email, Cpf, Role.ToString(), Password);
}