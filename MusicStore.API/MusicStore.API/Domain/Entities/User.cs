using MusicStore.API.Domain.Enumerations;

namespace MusicStore.API.Domain.Entities
{
    public class User : BaseEntity
    {
        public string Email { get; set; }
        
        public string Name { get; set; }
         
        public string Password { get; set; }

        public string PhoneNumber { get; set; }

        public enmUserStatus Status { get; set; }
    }
}
