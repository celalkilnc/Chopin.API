using System.Text.Json.Serialization;

namespace MusicStore.Application.Models.Request.Product;

public class mdlGetProductRequest : BaseRequest
{
    public Filters? Filters { get; set; }
}

public class Filters
{
    [JsonPropertyName("type")] public string? Type { get; set; }

    [JsonPropertyName("maxPrice")] public string? MaxPrice { get; set; }

    [JsonPropertyName("minPrice")] public string? MinPrice { get; set; }
}