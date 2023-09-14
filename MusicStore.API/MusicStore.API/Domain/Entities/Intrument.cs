using MusicStore.API.Domain.Enumerations;

namespace MusicStore.API.Domain.Entities
{
    public class Intrument : BaseEntity
    {
        public string Name { get; set; }

        public string Brand { get; set; }

        public double Price { get; set; }

        public InstrumentTypeEnum  Type { get; set; }

        public InstrumentCategoryEnum Category { get; set; }

    }
}
