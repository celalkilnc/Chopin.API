using System.Net.Mime;
using System.Reflection.Metadata;
using MusicStore.API.Domain.Enumerations;

namespace MusicStore.API.Domain.Entities
{
    public class Product : BaseEntity
    { 
        public string Name { get; set; }

        public string Brand { get; set; }

        public double Price { get; set; }

        public  enmStockStatus? StockStatus { get; set; }

        public enmInstrumentType  Type { get; set; }

        public enmInstrumentCategory Category { get; set; }

        public string? Description { get; set; }
    }
}
