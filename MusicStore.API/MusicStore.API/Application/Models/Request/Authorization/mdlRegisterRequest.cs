using System.Text.Json.Serialization;
using MusicStore.API.Domain.Enumerations;

namespace MusicStore.API.Application.Models.Request.Authorization;

public class mdlRegisterRequest : BaseRequest
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
    
    [JsonPropertyName("scndPassword")]
    public string ScndPassword { get; set; }
    
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }
 
}