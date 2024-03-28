namespace MusicStore.Application.Models.Response.Basket;

public class GetBasketByUserResponse : BaseResponse
{
    public Body Body { get; set; } 
    
    public override GetBasketByUserResponse Factory(bool success = false)
    {
        return new GetBasketByUserResponse()
        {
            Body = new(){ Basket = new()} ,
            Header = new() { Success = success, Messages = new() }
        };
    }
}

public class Body
{
    public List<Domain.Entities.Basket> Basket { get; set; }
}