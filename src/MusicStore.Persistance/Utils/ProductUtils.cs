using MusicStore.Application.Models.Request.Product;
using MusicStore.Persistance.Repositories.Media;
using MusicStore.Persistance.Repositories.Product;

namespace MusicStore.Persistance.Utils;

public class ProductUtils
{
    public static async Task<bool> TryToAddPhotos(mdlAddProductRequest pRequest, IPhotoRepository photoRepository, Guid Id)
    {
        bool isSuccess;
        try
        {
            foreach (var item in pRequest.Photos)
                await photoRepository.AddAsync(new()
                {
                    ID = Guid.NewGuid(),
                    ProductId = Id,
                    PhotoURL = item
                });

            await photoRepository.SaveAsync();
            isSuccess = true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            isSuccess = false;
        }

        return isSuccess;
    }

    public static async Task<bool> TryToAddProduct(mdlAddProductRequest pRequest, IProductRepository productRepository,
        Guid Id)
    {
        bool isProdAdded = false;
        try
        {
            isProdAdded = await productRepository.AddAsync(new()
            {
                ID = Id,
                Type = pRequest.IntrumentType,
                Brand = !string.IsNullOrEmpty(pRequest.Brand) ? pRequest.Brand : "",
                Price = pRequest.Price,
                Category = pRequest.Category,
                Name = pRequest.Name,
                Description = pRequest.Description
            });
            await productRepository.SaveAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            isProdAdded = false;
        }

        return isProdAdded;
    }
}