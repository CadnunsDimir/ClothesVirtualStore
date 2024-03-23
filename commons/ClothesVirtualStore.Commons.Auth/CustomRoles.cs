

namespace ClothesVirtualStore.Commons.Auth; 

public class CustomRoles
{
    public static readonly CustomRoles Admin = new CustomRoles("Admin");
    public static readonly CustomRoles Customer = new CustomRoles("Customer");
    private static readonly CustomRoles[] allRoles = [ Admin, Customer ];
    private string key;

    private CustomRoles(string key)
    {
        this.key = key;
    }

    public static CustomRoles Parse(string role) 
    {
        return allRoles.FirstOrDefault(x=> x.ToString() == role) ?? throw new ArgumentOutOfRangeException(role, "CustomRoles.Parse exception: invalid role");
    }

    public override string ToString() => key;
}