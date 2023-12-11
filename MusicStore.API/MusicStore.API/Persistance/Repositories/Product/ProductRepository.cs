namespace MusicStore.API.Persistance.Repositories.Product;

public class ProductRepository : Repository<Domain.Entities.Product>, IProductRepository
{
    public ProductRepository(MusicStoreDbContext context) : base(context)
    {
    }
}