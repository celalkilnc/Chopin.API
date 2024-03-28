using Microsoft.AspNetCore.Http;
using MusicStore.Application.Models.App;
using MusicStore.Application.Utils; 

namespace MusicStore.Application.Models.Request;

public class BaseRequest
{
    public mdlTokenInfo? TokenInfo {  get;  set; }

    public void FillTokenInfo(HttpContext context)
    {
        TokenInfo = TokenService.FieldTokenModel(context);
    }
}