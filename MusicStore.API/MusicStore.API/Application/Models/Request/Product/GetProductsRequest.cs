using System.Text.Json.Serialization;

namespace MusicStore.API.Application.Models.Request.Product;

public class GetProductsRequest : BaseRequest
{
    [JsonPropertyName("filters")]
    public Filters? Filters;
}

public class Filters
{
    
}