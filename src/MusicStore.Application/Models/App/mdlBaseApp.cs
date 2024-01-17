using System.Text.Json.Serialization;

namespace MusicStore.Application.Models.App;

public class mdlBaseApp
{
    [JsonIgnore]
    public string Action { get; set; }
}