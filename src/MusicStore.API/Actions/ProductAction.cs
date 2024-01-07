using MusicStore.API.Services;
using MusicStore.Application.Models.Request.Product;
using MusicStore.Application.Models.Response.Product;
using MusicStore.Application.Validations;
using MusicStore.Persistance.Repositories.Media;
using MusicStore.Persistance.Repositories.Product;

namespace MusicStore.API.Actions;

public class ProductAction : IProductService
{
    public async Task<mdlGetProductResponse> GetProducts(mdlGetProductRequest pRequest,
        IProductRepository productRepository, IPhotoRepository photoRepository)
    {
        return new() { };
    }

    public async Task<mdlAddProductResponse> AddProduct(mdlAddProductRequest pRequest,
        IProductRepository productRepository, IPhotoRepository photoRepository)
    {
        var res = new mdlAddProductResponse() { Header = new() { Messages = new(), Success = false } };
        var validRes = ProductValidator.AddProductValidate(pRequest);

        if (!validRes.Header.Success)
        {
            res.Header.Messages = validRes.Header.Messages;
            return res;
        }

        var prodID = Guid.NewGuid();

        var isProdAdded = await productRepository.AddAsync(new()
        {
            ID = prodID,
            Type = pRequest.IntrumentType,
            Brand = pRequest.Brand,
            Price = pRequest.Price,
            Category = pRequest.Category,
            Name = pRequest.Name,
            Description = pRequest.Description
        });

        if (isProdAdded)
            foreach (var item in pRequest.Photos)
                await photoRepository.AddAsync(new()
                {
                    ID = Guid.NewGuid(),
                    ProductId = prodID,
                    PhotoURL = item
                });

        await photoRepository.SaveAsync();
        await productRepository.SaveAsync();

        return res;
    }
}