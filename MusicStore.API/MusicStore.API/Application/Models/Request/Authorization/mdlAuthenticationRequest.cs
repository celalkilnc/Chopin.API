using System.Text.Json.Serialization;

namespace MusicStore.API.Application.Models.Request.Authorization;

public class mdlAuthenticationRequest : BaseRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; }
    
    [JsonPropertyName("password")]
    public string Password { get; set; }
}