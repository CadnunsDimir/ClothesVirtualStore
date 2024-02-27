using System.ComponentModel.DataAnnotations.Schema;

namespace ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;

[Table("customers")]
public class CustomerEntity
{
    public CustomerEntity(string cpf, string fullName)
    {
        Id = Guid.NewGuid();
        Cpf = cpf;
        FullName = fullName;
    }
    
    public Guid Id { get; set; }
    public string Cpf { get; set; }
    public string FullName { get; set; }
}