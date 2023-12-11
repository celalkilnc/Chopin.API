using System.Text.Json.Serialization;
using MusicStore.API.Domain.Enumerations;

namespace MusicStore.API.Application.Models.Response.Authorization;

public class mdlAuthenticationResponse : BaseResponse
{
    [JsonPropertyName("body")] public Body Body { get; set; }
}

public class Body
{
    [JsonPropertyName("token")] public string Token { get; set; }

    [JsonPropertyName("expression")] public DateTime Expression { get; set; }

    [JsonPropertyName("authorityLevel")] public enmUserStatus AuthorityLevel { get; set; }
}