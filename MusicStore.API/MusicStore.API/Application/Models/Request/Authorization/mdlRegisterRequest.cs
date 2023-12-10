namespace MusicStore.API.Application.Models.Request.Authorization;

public class mdlRegisterRequest : BaseRequest
{
    public string UserName { get; set; }
    
    public string Email { get; set; }
    
    public string Password { get; set; }
}