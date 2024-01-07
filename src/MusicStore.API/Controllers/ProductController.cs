using Microsoft.AspNetCore.Mvc;
using MusicStore.API.Services;
using MusicStore.Application.Attributes;
using MusicStore.Application.Models.Request.Product;
using MusicStore.Application.Models.Response.Product;
using MusicStore.Application.Utils;
using MusicStore.Application.Utils.AppSetting;
using MusicStore.Persistance.Repositories.Media;
using MusicStore.Persistance.Repositories.Product;

namespace MusicStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : Controller, IProductService
{
    private readonly IProductService _productService;
    private readonly IProductRepository _productRepository;
    private readonly IAppSetting _appSetting;
    private readonly IPhotoRepository _photoRepository;

    public ProductController(IProductService productService, IProductRepository productRepository,
        IAppSetting appSetting, IPhotoRepository photoRepository)
    {
        _productService = productService;
        _productRepository = productRepository;
        _appSetting = appSetting;
        _photoRepository = photoRepository;
    }

    [HttpGet("GetProducts")]
    [JwtAuthorize("")]
    public async Task<mdlGetProductResponse> GetProducts(mdlGetProductRequest pRequest,
        IProductRepository? productRepository, IPhotoRepository photoRepository)
    {
        return await _productService.GetProducts(pRequest, _productRepository, _photoRepository);
    }

    [HttpPost("AddProduct")]
    [JwtAuthorize("Admin")]
    public async Task<mdlAddProductResponse> AddProduct(mdlAddProductRequest pRequest,
        IProductRepository? productRepository, IPhotoRepository photoRepository)
    {
        pRequest.FillTokenInfo(HttpContext);
        return await _productService.AddProduct(pRequest, _productRepository, _photoRepository);
    }
}