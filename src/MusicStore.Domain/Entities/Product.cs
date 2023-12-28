using MusicStore.Domain.Enumerations;

namespace MusicStore.Domain.Entities;

public class Product : BaseEntity
{
    public string Name { get; set; }

    public string Brand { get; set; }

    public double Price { get; set; }

    public  enmStockStatus? StockStatus { get; set; }

    public enmInstrument  Type { get; set; }

    public enmInstrumentCategory Category { get; set; }

    public string? Description { get; set; }
}