using MusicStore.Application.Models.App;

namespace MusicStore.Application.Models.Request;

public class BaseRequest
{
    public mdlTokenInfo? TokenInfo {  get;  set; }
}