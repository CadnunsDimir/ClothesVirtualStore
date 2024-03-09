
using System.Text.Json;
using ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Models;
using RestSharp;

namespace ClothesVirtualStore.MicroServices.Consumers.CartCheckout.Data.Repositories;

public class StoreProductsRepository : IStoreProductsRepository
{
    private readonly string _apiUrl;

    public StoreProductsRepository(IConfiguration configuration)
    {
        _apiUrl = configuration.GetConnectionString(AppConstants.ProductsApiKey) ?? "";
    }
    public List<StoreProductsEntity> QueryByIds(Guid[] idsAArray)
    {
        return idsAArray.Select(GetById).ToList();
    }

    private StoreProductsEntity GetById(Guid id){
        var client = new RestClient($"{_apiUrl}/{id}");
        var request = new RestRequest
        {
            Method = Method.Get
        };
        var response = client.Execute<StoreProductsEntity>(request);
        return response.Data ?? throw new Exception("IStoreProductsRepository.GetById has returned null");
    }
}



