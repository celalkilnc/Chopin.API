using System.Text.Json.Serialization;

namespace MusicStore.API.Application.Models.Response;

public class BaseResponse
{
    public Header Header { get; set; }
}

public class Header
{
    [JsonPropertyName("messages")]
    public List<string> Messages { get; set; }

    [JsonPropertyName("success")]
    public bool Success { get; set; }
    
    [JsonPropertyName("requestDate")]
    public DateTime RequestDate { get; set; }
}