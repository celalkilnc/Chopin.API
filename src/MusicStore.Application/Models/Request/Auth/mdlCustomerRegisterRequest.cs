using System.Text.Json.Serialization;

namespace MusicStore.Application.Models.Request;

public class mdlCustomerRegisterRequest : mdlBaseAuthRequest
{
    [JsonPropertyName("userName")]
    public string UserName { get; set; }
    
    [JsonPropertyName("phoneNumber")]
    public string? PhoneNumber { get; set; }
    
    [JsonPropertyName("passwordCheck")]
    public string PasswordCheck { get; set; }
}