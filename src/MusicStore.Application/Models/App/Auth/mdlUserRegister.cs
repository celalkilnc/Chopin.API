using MusicStore.Application.Models.Request;
using MusicStore.Domain.Enumerations;

namespace MusicStore.Application.Models.App.Auth;

public class mdlUserRegister:mdlBaseAuthRequest
{
    public enmUserStatus UserStatus { get; set; }
}