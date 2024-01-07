using System.Text.Json.Serialization;
using MusicStore.Domain.Enumerations;

namespace MusicStore.Application.Models.Request.Product;

public class mdlAddProductRequest : BaseRequest
{
    [JsonPropertyName("brand")]
    public string? Brand { get; set; }
    
    [JsonPropertyName("intrumentType")]
    public enmInstrument IntrumentType { get; set; }
    
    [JsonPropertyName("category")]
    public enmInstrumentCategory Category { get; set; }
    
    [JsonPropertyName("price")]
    public double Price { get; set; }
    
    [JsonPropertyName("description")]
    public string? Description { get; set; }
    
    [JsonPropertyName("photos")]
    public List<string>? Photos { get; set; }
    
    [JsonPropertyName("name")]
    public string Name { get; set; }
    
    [JsonPropertyName("stockStatus")]
    public enmStockStatus? StockStatus { get; set; }
}