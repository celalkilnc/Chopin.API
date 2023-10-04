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
  
        public InstrumentTypeEnum  Type { get; set; }

        public InstrumentCategoryEnum Category { get; set; }

        public string? Description { get; set; }
    }
}
