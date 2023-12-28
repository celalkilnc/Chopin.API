using MusicStore.Application.Models.Response;

namespace MusicStore.Application.Utils;

public class ClassFactory
{
    public static BaseResponse BaseResponseFactory(bool successDefault = false)
    {
        return new()
            { Header = new Header() { Messages = new(), Success = successDefault } };
    }

    public static BaseResponse BaseResponseFactory(List<string> messages, bool successDefault = false)
    {
        var res = BaseResponseFactory(successDefault);
        messages.ForEach(x => res.Header.Messages.Add(x));
        return res;
    }
}