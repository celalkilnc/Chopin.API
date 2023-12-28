using System.Text.Json.Serialization;

namespace MusicStore.Application.Models.Request;

public class mdlBaseAuthRequest : BaseRequest
{
    [JsonPropertyName("email")]
    public string Email { get; set; }

    [JsonPropertyName("password")]
    public string Password { get; set; }
}