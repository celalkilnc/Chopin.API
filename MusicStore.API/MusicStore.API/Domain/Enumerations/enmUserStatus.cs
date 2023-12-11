using System.ComponentModel;

namespace MusicStore.API.Domain.Enumerations
{
    public enum enmUserStatus
    {
        [Description("Customer")]
        Customer = 0,
        
        [Description("PrimeCustomer")]
        PrimeCustomer = 1,//Maybe
        
        [Description("Admin")]
        Admin = 2,
        
        [Description("Banned")]
        Banned = 3,
        
        [Description("MasterAdmin")]
        MasterAdmin = 4,
    }
}
