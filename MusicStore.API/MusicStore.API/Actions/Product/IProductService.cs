using MusicStore.API.Application.Models.Request.Product;
using MusicStore.API.Application.Models.Response.Product;

namespace MusicStore.API.Actions.Product;

public interface IProductService
{
   public Task<GetProductResponse> GetProducts(GetProductsRequest pRequest);
   
   public Task<GetProductResponse> AddProduct(GetProductsRequest pRequest);
   
   
}