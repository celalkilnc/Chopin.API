﻿using System.Text.Json.Serialization;
using MusicStore.Domain.Enumerations;

namespace MusicStore.Application.Models.Response.Auth;

public class mdlAuthResponse : BaseResponse
{
    [JsonPropertyName("body")]
    public Body Body { get; set; }

    public override mdlAuthResponse Factory(bool success = false)
    {
        return new() { Body = new(),Header = new(){Messages = new(),Success = success}};
    }
}

public class Body
{
    [JsonPropertyName("token")]
    public string Token { get; set; }

    [JsonPropertyName("expire")]
    public DateTime Expire { get; set; }

    [JsonPropertyName("userStatus")]
    public string UserStatus { get; set; }
}