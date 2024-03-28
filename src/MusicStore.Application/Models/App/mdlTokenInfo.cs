using MusicStore.Domain.Entities;
using MusicStore.Domain.Enumerations;

namespace MusicStore.Application.Models.App;

public class mdlTokenInfo : mdlBaseApp
{
    private enmUserStatus role;

    public string Email { get; set; }

    public string Name { get; set; }

    public User User { get; set; }

    public enmUserStatus Role
    {
        get { return role; }
    }

    public void SetRoleStrToEnm(string pRole)
    {
        switch (pRole)
        {
            case "Customer":
                role = enmUserStatus.Customer;
                break;
            case "Admin":
                role = enmUserStatus.Admin;
                break;
            case "MasterAdmin":
                role = enmUserStatus.MasterAdmin;
                break;
            case "PrimeCustomer":
                role = enmUserStatus.PrimeCustomer;
                break;
            default:
                role = enmUserStatus.Banned;
                break;
        }
    }
}