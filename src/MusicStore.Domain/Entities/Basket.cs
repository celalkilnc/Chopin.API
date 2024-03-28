namespace MusicStore.Domain.Entities;

public class Basket : BaseEntity
{
    public Guid UserId { get; set; }

    public Guid ProductId { get; set; }

    public int ProductNum { get; set; }
}