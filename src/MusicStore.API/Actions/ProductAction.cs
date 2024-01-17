using MusicStore.API.Services;
using MusicStore.Application.Models.App.Product;
using MusicStore.Application.Models.Request.Product;
using MusicStore.Application.Models.Response.Product;
using MusicStore.Application.Utils;
using MusicStore.Application.Validations;
using MusicStore.Domain.Entities;
using MusicStore.Persistance.Repositories.Media;
using MusicStore.Persistance.Repositories.Product;
using MusicStore.Persistance.Utils;

namespace MusicStore.API.Actions;

public class ProductAction : IProductService
{
    public async Task<mdlGetProductResponse> GetProducts(mdlGetProductRequest pRequest,
        IProductRepository productRepository, IPhotoRepository photoRepository)
    {
        var res = new mdlGetProductResponse().Factory(false);
        
        var allProducts = ApplyFilter(productRepository.GetAll().ToList(), pRequest.Filters);
        if (allProducts.Count > 0 && allProducts != null)
        {
            var allPhotos = photoRepository.GetAll().ToList();

            var compiledProducts = SyncProductsAndPhotos(allProducts, allPhotos);
            res.Body.Products = compiledProducts;
            res.Header.Success = true;
            res.Header.Messages.Add(ResponseMessages.Successfuly);
        }
        else 
            res.Header.Messages.Add(ResponseMessages.ProdNotFound);
        
        return res;
    }
 
    public async Task<mdlAddProductResponse> AddProduct(mdlAddProductRequest pRequest,
        IProductRepository productRepository, IPhotoRepository photoRepository)
    {
        var res = new mdlAddProductResponse().Factory();
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

    public async Task<mdlGetProductDetailResponse> GetProductDetail(mdlGetProductDetailRequest pRequest,
        IProductRepository productRepository,
        IPhotoRepository photoRepository)
    {
        var res = new mdlGetProductDetailResponse().Factory();
        var product = productRepository.GetWhere(x => x.ID == pRequest.ProductId).FirstOrDefault();
        if (product != null)
        {
            res.Product = new()
            {
                ID = product.ID,
                Price = product.Price,
                Brand = product.Brand,
                Name = product.Name,
                Photos = mdlPhoto.PhotoEntityToModel(photoRepository.GetWhere(x => x.ProductId == product.ID).ToList()),
                Description = !string.IsNullOrEmpty(product.Description) ? product.Description : null,
            };

            res.Header.Messages.Add(ResponseMessages.Successfuly);
            res.Header.Success = true;
        }
        else
            res.Header.Messages.Add(ResponseMessages.ProdNotFound);

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
                Price = product.Price,
                Brand = product.Brand,
                Name = product.Name,
                Photos = compiledPhotos
            });
        }

        return res;
    }
}