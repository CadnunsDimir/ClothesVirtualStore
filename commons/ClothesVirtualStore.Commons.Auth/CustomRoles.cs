namespace ClothesVirtualStore.Commons.Auth; 

public class CustomRoles
{
    public static readonly CustomRoles Admin = new CustomRoles("Admin");
    private string key;

    private CustomRoles(string key)
    {
        this.key = key;
    }

    public override string ToString() => key;
}