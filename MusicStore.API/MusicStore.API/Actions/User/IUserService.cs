using Microsoft.AspNetCore.Mvc;
using MusicStore.API.Application.Models.Request.Authorization;
using MusicStore.API.Application.Models.Response;
using MusicStore.API.Application.Models.Response.Authorization;
using MusicStore.API.Persistance.Repositories;

namespace MusicStore.API.Actions;

public interface IUserService
{
    public Task<BaseResponse> Register(mdlRegisterRequest pRequest,IUserRepository userRepository);

    public Task<mdlAuthenticationResponse> Authentication(mdlAuthenticationRequest pRequest, IUserRepository userRepository);
}