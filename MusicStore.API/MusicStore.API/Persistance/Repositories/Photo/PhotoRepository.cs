namespace MusicStore.API.Persistance.Repositories.Photo;

public class PhotoRepository : Repository<Domain.Entities.Photo>, IPhotoRepository
{
    public PhotoRepository(MusicStoreDbContext context) : base(context)
    {
    }
}