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
    
    [JsonPropertyName("description")] 
    public string? Description { get; set; }
 
    [JsonPropertyName("phtos")]
    public List<mdlPhoto>? Photos { get; set; } 
}

public class mdlPhoto
{
    public Guid ProductId { get; set; }

    public string PhotoURL { get; set; }

    
    public static mdlPhoto PhotoEntityToModel(Domain.Entities.Photo photo)
    {
        return new()
        {
            ProductId = photo.ProductId,
            PhotoURL = photo.PhotoURL
        };
    }
    
    public static List<mdlPhoto> PhotoEntityToModel(List<Domain.Entities.Photo> Photos)
    {
        var res = new List<mdlPhoto>();
        foreach (var photo in Photos) 
            res.Add(PhotoEntityToModel(photo));
        
        return res;
    }
    
}