using System.Text.Json.Serialization;
using MusicStore.Application.Models.App.Product;

namespace MusicStore.Application.Models.Response.Product;

public class mdlGetProductResponse : BaseResponse
{
    [JsonPropertyName("body")]
    public Body Body { get; set; }
}

public class Body
{
    [JsonPropertyName("products")]
    public List<mdlProduct> Products { get; set; }
}