using ClothesVirtualStore.Commons.Auth;
using Microsoft.EntityFrameworkCore;

namespace ClothesVirtualStore.Commons.Auth.Data;
public interface IUserDbContext
{
    DbSet<UserEntity> Users { get; }

    void SaveOnDb();
}