using MusicStore.Application.Models.Request.Product;
using MusicStore.Application.Models.Response.Product;
using MusicStore.Persistance.Repositories.Product;

namespace MusicStore.API.Services;

public interface IProductService
{
   public Task<mdlGetProductResponse> GetProducts(mdlGetProductRequest pRequest, IProductRepository productRepository);

   public Task<mdlAddProductResponse> AddProduct(mdlAddProductRequest pRequest, IProductRepository productRepository);
}