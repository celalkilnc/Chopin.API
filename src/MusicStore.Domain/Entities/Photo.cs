namespace MusicStore.Domain.Entities;

public class Photo : BaseEntity
{
    public Guid ProductId { get; set; }

    public string PhotoURL { get; set; }
}