namespace MusicStore.Persistance.Repositories.Media;

public class PhotoRepository : Repository<Domain.Entities.Photo>, IPhotoRepository
{
    public PhotoRepository(MSDBContext context) : base(context)
    {
    }
}