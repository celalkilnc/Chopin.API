namespace MusicStore.API.Application.Models.Response.Product;

public class GetProductResponse : BaseResponse
{
    public List<ProductResponse> Products { get; set; }

    public Dictionary<string,object> Filters { get; set; }
}