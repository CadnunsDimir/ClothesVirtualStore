
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;

namespace ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Repositories;

public interface IStoreProductsRepository
{
    List<StoreProductsEntity> QueryByIds(Guid[] idsAArray);
}
