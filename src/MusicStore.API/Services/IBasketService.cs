using MusicStore.Application.Models.Request.Basket;
using MusicStore.Application.Models.Response.Basket;
using MusicStore.Persistance.Repositories.Basket;
using MusicStore.Persistance.Repositories.User;

namespace MusicStore.API.Services;

public interface IBasketService
{
    public Task<GetBasketByUserResponse> GetBasketByUser(mdlGetBasketByUserRequest pRequest,IBasketRepository? basketRepository,IUserRepository? userRepository);
}