using System.Text.Json.Serialization;

namespace MusicStore.Application.Models.Response;

public class BaseResponse
{
    [JsonPropertyName("header")]
    public Header Header { get; set; }

    public virtual BaseResponse Factory(bool success = false)
    {
        return new() { Header = new() { Messages = new(), Success = success } }; 
    }
}

public class Header
{
    [JsonPropertyName("messages")]
    public List<string> Messages { get; set; }
    
    [JsonPropertyName("success")]
    public bool Success { get; set; }
}