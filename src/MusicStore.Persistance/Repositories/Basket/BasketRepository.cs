namespace MusicStore.Persistance.Repositories.Basket;

public class BasketRepository : Repository<Domain.Entities.Basket>, IBasketRepository
{
    public BasketRepository(MSDBContext context) : base(context)
    {
    }
}