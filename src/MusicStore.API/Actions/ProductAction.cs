using Microsoft.EntityFrameworkCore.ChangeTracking;
using MusicStore.API.Services;
using MusicStore.Application.Models.App.Product;
using MusicStore.Application.Models.Request.Product;
using MusicStore.Application.Models.Response.Product;
using MusicStore.Application.Utils;
using MusicStore.Application.Validations;
using MusicStore.Domain.Entities;
using MusicStore.Domain.Enumerations;
using MusicStore.Persistance.Repositories.Media;
using MusicStore.Persistance.Repositories.Product;
using MusicStore.Persistance.Utils;

namespace MusicStore.API.Actions;

public class ProductAction : IProductService
{
    public async Task<mdlGetProductResponse> GetProducts(mdlGetProductRequest pRequest,
        IProductRepository productRepository, IPhotoRepository photoRepository)
    {
        var allProducts = ApplyFilter(productRepository.GetAll().ToList(), pRequest.Filters);
        var allPhotos = photoRepository.GetAll().ToList();

        var compiledProducts = SyncProductsAndPhotos(allProducts, allPhotos);

        return new()
        {
            Body = new()
            {
                Products = compiledProducts
            },
            Header = new()
            {
                Success = true,
                Messages = new() { ResponseMessages.Successfuly }
            }
        };
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

        var isAdded = await ProductUtils.TryToAddProduct(pRequest, productRepository, prodID);
        if (isAdded && (pRequest.Photos != null || pRequest.Photos.Count > 0))
            isAdded = await ProductUtils.TryToAddPhotos(pRequest, photoRepository, prodID);

        res.Header.Messages.Add(isAdded ? ResponseMessages.Successfuly : ResponseMessages.Failed);
        res.Header.Success = isAdded;

        return res;
    }

    private List<Product> ApplyFilter(List<Product> products, Filters? filters)
    {
        if (filters == null)
            return products;
        if (filters.Type != null)
            products = products.Where(x => x.Type == filters.Type).ToList();
        if (filters.MinPrice != 0.0)
            products = products.Where(x => x.Price > filters.MinPrice).ToList();
        if (filters.MaxPrice != 0.0)
            products = products.Where(x => x.Price < filters.MaxPrice).ToList();

        return products;
    }

    private List<mdlProduct> SyncProductsAndPhotos(List<Product> products, List<Domain.Entities.Photo> photos)
    {
        var res = new List<mdlProduct>();
        foreach (var product in products)
        {
            var selfPhotos = photos.Where(x => x.ProductId == product.ID);
            var compiledPhotos = new List<mdlPhoto>();
            foreach (var photo in selfPhotos)
                compiledPhotos.Add(new() { PhotoURL = photo.PhotoURL, ProductId = photo.ProductId });

            res.Add(new()
            {
                ID = product.ID,
                Description = !string.IsNullOrEmpty(product.Description) ? product.Description : "",
                Price = product.Price,
                Brand = product.Brand,
                Category = product.Category,
                Name = product.Name,
                Photos = compiledPhotos,
                Type = product.Type,
                StockStatus = product.StockStatus == null ? enmStockStatus.Unspecified : product.StockStatus
            });
        }

        return res;
    }
}