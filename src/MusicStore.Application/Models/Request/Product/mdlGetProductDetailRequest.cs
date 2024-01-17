using System.Text.Json.Serialization;

namespace MusicStore.Application.Models.Request.Product;

public class mdlGetProductDetailRequest : BaseRequest
{
    [JsonPropertyName("productId")]
    public Guid ProductId { get; set; }
}