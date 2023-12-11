using Microsoft.AspNetCore.Mvc;
using MusicStore.API.Actions;
using MusicStore.API.Application.Models.Request.Authorization;
using MusicStore.API.Application.Models.Response;
using MusicStore.API.Application.Models.Response.Authorization;
using MusicStore.API.Persistance.Repositories;

namespace MusicStore.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : Controller , IUserService
{
    private readonly IUserService _userService;
    private readonly IUserRepository _userRepository;
 
    public UserController(IUserService userService, IUserRepository userRepository)
    {
        _userService = userService;
        _userRepository = userRepository;
    }

    [HttpPost("Register")] 
    public Task<BaseResponse> Register(mdlRegisterRequest pRequest, IUserRepository userRepository)
    {
        return _userService.Register(pRequest, userRepository);
    }
    
    [HttpPost("Authentication")] 
    public Task<mdlAuthenticationResponse> Authentication(mdlAuthenticationRequest pRequest, IUserRepository userRepository)
    {
        return _userService.Authentication(pRequest, userRepository);
    }
}