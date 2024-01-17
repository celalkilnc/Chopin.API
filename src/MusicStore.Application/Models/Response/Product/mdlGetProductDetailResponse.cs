using MusicStore.Application.Models.App.Product;

namespace MusicStore.Application.Models.Response.Product;

public class mdlGetProductDetailResponse : BaseResponse
{
    public mdlProduct? Product { get; set; }

    public override mdlGetProductDetailResponse Factory(bool success = false)
    {
        return new() { Header = new() { Messages = new(), Success = success }, Product = new() };
    }
}