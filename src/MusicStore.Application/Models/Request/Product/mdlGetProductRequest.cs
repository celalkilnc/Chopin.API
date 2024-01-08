using System.Text.Json.Serialization;
using MusicStore.Domain.Enumerations;

namespace MusicStore.Application.Models.Request.Product;

public class mdlGetProductRequest : BaseRequest
{
    [JsonPropertyName("filters")]
    public Filters? Filters { get; set; }
}

public class Filters
{
    [JsonPropertyName("type")] public enmInstrument? Type { get; set; }

    [JsonPropertyName("maxPrice")] public double? MaxPrice { get; set; }

    [JsonPropertyName("minPrice")] public double? MinPrice { get; set; }
}