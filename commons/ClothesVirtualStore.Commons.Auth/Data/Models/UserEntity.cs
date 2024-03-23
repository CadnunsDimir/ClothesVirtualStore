namespace ClothesVirtualStore.Commons.Auth;
public record UserEntity(Guid Id, string UserName, string Email, string Cpf, string Role, string Password)
{
    public bool IsValid() => 
        UserName.Length > 3 &&
        Email.Length > 7 &&
        Email.Contains("@") &&
        Cpf.Length == 11 && 
        !string.IsNullOrEmpty(Role) &&
        Password.Length >=5;
}
