namespace MusicStore.Persistance.Repositories.Product;

public class ProductRepository: Repository<Domain.Entities.Product>, IProductRepository
{
    public ProductRepository(MSDBContext context) : base(context)
    {
    }
}