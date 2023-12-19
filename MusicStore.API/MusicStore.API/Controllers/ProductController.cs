using Microsoft.AspNetCore.Mvc;
using MusicStore.API.Actions.Product;
using MusicStore.API.Application.Models.Request.Product;
using MusicStore.API.Application.Models.Response;
using MusicStore.API.Application.Models.Response.Product;

namespace MusicStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller, IProductService
{
    private readonly IProductService _productService;

    public ProductController(IProductService productService)
    {
        _productService = productService;
    }

    [HttpPost("GetAllProducts")]
    public async Task<GetProductResponse> GetProducts(GetProductsRequest pRequest)
    {
        return new();
    }

    public Task<GetProductResponse> AddProduct(GetProductsRequest pRequest)
    {
        throw new NotImplementedException();
    }
}