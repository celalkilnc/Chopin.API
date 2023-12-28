using MusicStore.Application.Models.Request;
using MusicStore.Application.Models.Response;
using MusicStore.Application.Models.Response.Auth;
using MusicStore.Persistance.Repositories.User;

namespace MusicStore.API.Services;

public interface IAuthService
{
     public Task<mdlAuthResponse> Authentication(mdlAuthenticationRequest pRequest, IUserRepository? userRepository);

     public Task<BaseResponse> CustomerRegister(mdlCustomerRegisterRequest pRequest, IUserRepository? userRepository);
}