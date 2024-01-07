using MusicStore.API.Services;
using MusicStore.Application.Models.Request.Product;
using MusicStore.Application.Models.Response.Product;
using MusicStore.Application.Validations;
using MusicStore.Persistance.Repositories.Product;

namespace MusicStore.API.Actions;

public class ProductAction : IProductService
{
    public async Task<mdlGetProductResponse> GetProducts(mdlGetProductRequest pRequest,
        IProductRepository productRepository)
    { 
        return new() { };
    }

    public async Task<mdlAddProductResponse> AddProduct(mdlAddProductRequest pRequest,
        IProductRepository productRepository)
    {
        var res = new mdlAddProductResponse(){Header = new(){Messages = new(),Success = false}}; 
        var validRes = ProductValidator.AddProductValidate(pRequest);

        if (!validRes.Header.Success)
        {
            res.Header.Messages = validRes.Header.Messages;
            return res;
        }
        
        return new() { };
    }
}