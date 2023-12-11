using MusicStore.API.Domain.Entities;

namespace MusicStore.API.Persistance.Repositories;

public class UserRepository : Repository<User>, IUserRepository
{
    public UserRepository(MusicStoreDbContext context) : base(context) {  }
}