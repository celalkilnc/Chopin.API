namespace MusicStore.Persistance.Repositories.User;

public class UserRepository: Repository<Domain.Entities.User>, IUserRepository
{
    public UserRepository(MSDBContext context) : base(context) {  }
}
 