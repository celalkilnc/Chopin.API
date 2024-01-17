namespace MusicStore.Application.Models.Response.Product;

public class mdlAddProductResponse : BaseResponse
{
    public override mdlAddProductResponse Factory(bool success = false)
    {
        return new mdlAddProductResponse()
        {
            Header = new() { Success = success, Messages = new() }
        };
    }
}