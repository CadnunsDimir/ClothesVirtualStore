

namespace ClothesVirtualStore.Commons.Auth; 
public class User 
{ 
    public required string UserName { get; set; }
    public required string Email { get; set; }
    public required CustomRoles Role { get; set; } 
    public required string Cpf { get; set; }

    public static User? Map(UserEntity? userEntity) {
        if (userEntity is UserEntity)
        {
            return new User
            {
                Cpf = userEntity.Cpf,
                Email = userEntity.Email,
                Role = CustomRoles.Parse(userEntity.Role),
                UserName = userEntity.UserName
            };
        }

        return null;
    }
};