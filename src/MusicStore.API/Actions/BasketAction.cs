using MusicStore.API.Services;
using MusicStore.Application.Models.Request.Basket;
using MusicStore.Application.Models.Response.Basket;
using MusicStore.Persistance.Repositories.Basket;
using MusicStore.Persistance.Repositories.User;

namespace MusicStore.API.Actions;

public class BasketAction : IBasketService
{
    public async Task<GetBasketByUserResponse> GetBasketByUser(mdlGetBasketByUserRequest pRequest,IBasketRepository? basketRepository,IUserRepository? userRepository)
    {
        var res = new GetBasketByUserResponse().Factory();
        res.Body.Basket = basketRepository.GetWhere(x => x.UserId == pRequest.TokenInfo.User.ID).ToList();
        return res;
    }
}