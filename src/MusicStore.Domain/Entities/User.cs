using MusicStore.Domain.Enumerations;

namespace MusicStore.Domain.Entities;

public class User : BaseEntity
{
    public string Email { get; set; }
        
    public string Name { get; set; }
         
    public string Password { get; set; }

    public string PhoneNumber { get; set; }

    public enmUserStatus Status { get; set; }
}