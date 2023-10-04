namespace MusicStore.API.Domain.Entities;

public class Photo : BaseEntity
{
    public Guid ProductId { get; set; }

    public string PhotoURI { get; set; }
    
}