
namespace ClothesVirtualStore.Commons.Auth;

public class CustomerUser
{
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required string Cpf { get; set; }
    public required string Password { get; set; }

    internal UserEntity ToEntity() => new (Guid.NewGuid(), UserName, Email, Cpf, CustomRoles.Customer.ToString(), Password);
}