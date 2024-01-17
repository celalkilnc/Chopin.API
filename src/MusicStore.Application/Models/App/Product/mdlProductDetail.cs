using System.Text.Json.Serialization;
using MusicStore.Domain.Enumerations;

namespace MusicStore.Application.Models.App.Product;

public class mdlProductDetail : mdlProduct
{
    
    [JsonPropertyName("stockStatus")] 
    public  enmStockStatus? StockStatus { get; set; }

    [JsonPropertyName("type")] 
    public enmInstrument  Type { get; set; }
    
    [JsonPropertyName("category")] 
    public enmInstrumentCategory Category { get; set; }

    [JsonPropertyName("description")] 
    public string? Description { get; set; }
}