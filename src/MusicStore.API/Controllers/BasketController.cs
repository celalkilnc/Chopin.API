using Microsoft.AspNetCore.Mvc;
using MusicStore.API.Helper;
using MusicStore.API.Services;
using MusicStore.Application.Attributes;
using MusicStore.Application.Models.Request.Basket;
using MusicStore.Application.Models.Response.Basket;
using MusicStore.Persistance.Repositories.Basket;
using MusicStore.Persistance.Repositories.User;

namespace MusicStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BasketController : Controller, IBasketService
{
    private readonly IBasketService _basketService;
    private readonly IUserRepository _userRepository;
    private readonly IBasketRepository _basketRepository;

    public BasketController(IBasketService basketService, IUserRepository userRepository, IBasketRepository basketRepository)
    {
        _basketService = basketService;
        _userRepository = userRepository;
        _basketRepository = basketRepository;
    }

    [HttpGet("get/fromUser")]
    [JwtAuthorize("Customer,Admin")]
    public async Task<GetBasketByUserResponse> GetBasketByUser(mdlGetBasketByUserRequest pRequest,
        IBasketRepository? basketRepository, IUserRepository? userRepository)
    {
        pRequest.FillTokenInfo(HttpContext);
        pRequest.TokenInfo.User = GetDataHelper.GetUserByMail(userRepository, pRequest.TokenInfo.Email);
        return await _basketService.GetBasketByUser(pRequest, _basketRepository, _userRepository);
    }
}