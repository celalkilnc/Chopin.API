using MusicStore.Domain.Entities;
using MusicStore.Persistance.Repositories.User;

namespace MusicStore.API.Helper;

public class GetDataHelper
{
    public  static  User GetUserByMail(IUserRepository userRepository, string email)
    {
        return userRepository.GetWhere(x => x.Email == email).FirstOrDefault(); 
    }
}