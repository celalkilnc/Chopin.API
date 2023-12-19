using MusicStore.API.Application.Models.Request.Product;
using MusicStore.API.Application.Models.Response.Product;

namespace MusicStore.API.Actions.Product;

public class ProductAction : IProductService
{
    
    public async Task<GetProductResponse> GetProducts(GetProductsRequest pRequest)
    {
        return new();
    }

    public Task<GetProductResponse> AddProduct(GetProductsRequest pRequest)
    {
        throw new NotImplementedException();
    }
}