using System.Text.Json.Serialization;
using MusicStore.Domain.Enumerations;

namespace MusicStore.Application.Models.App.Product;

public class mdlProduct : mdlBaseApp
{
    [JsonPropertyName("id")]
    public Guid ID { get; set; }
    
    [JsonPropertyName("name")] 
    public string Name { get; set; }

    [JsonPropertyName("brand")] 
    public string Brand { get; set; }

    [JsonPropertyName("price")] 
    public double Price { get; set; }

    [JsonPropertyName("stockStatus")] 
    public  enmStockStatus? StockStatus { get; set; }

    [JsonPropertyName("type")] 
    public enmInstrument  Type { get; set; }

    [JsonPropertyName("category")] 
    public enmInstrumentCategory Category { get; set; }

    [JsonPropertyName("description")] 
    public string? Description { get; set; }
}