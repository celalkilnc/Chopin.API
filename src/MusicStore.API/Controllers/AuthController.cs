using Microsoft.AspNetCore.Mvc;
using MusicStore.API.Services;
using MusicStore.Application.Models.Request;
using MusicStore.Application.Models.Response;
using MusicStore.Application.Models.Response.Auth;
using MusicStore.Persistance.Repositories.User;

namespace MusicStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : Controller , IAuthService
{
    private readonly IAuthService _authService;
    private readonly IUserRepository _userRepository;

    public AuthController(IAuthService authService, IUserRepository userRepository)
    {
        _authService = authService;
        _userRepository = userRepository;
    }

    [HttpGet("Authentication")]
    public async Task<mdlAuthResponse> Authentication(mdlAuthenticationRequest pRequest, IUserRepository? userRepository)
    {
        return await _authService.Authentication(pRequest, _userRepository);
    } 

    [HttpPost("Register")]
    public async Task<BaseResponse> CustomerRegister(mdlCustomerRegisterRequest pRequest, IUserRepository? userRepository)
    {
        return await _authService.CustomerRegister(pRequest, _userRepository);
    }
}