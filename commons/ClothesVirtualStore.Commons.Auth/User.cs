namespace ClothesVirtualStore.Commons.Auth; 
public record User (string UserName, string Email, CustomRoles Role, string Password);