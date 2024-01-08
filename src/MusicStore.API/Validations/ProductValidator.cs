using MusicStore.Application.Models.Request.Product;
using MusicStore.Application.Models.Response;
using MusicStore.Application.Utils;

namespace MusicStore.Application.Validations;

public class ProductValidator
{
    public static BaseResponse AddProductValidate(mdlAddProductRequest pRequest)
    {
        var res = ClassFactory.BaseResponseFactory(true);

        if (res.Header.Messages.Count > 0)
            res.Header.Success = false;
            
        return res;
    }
    
    public static BaseResponse GetProductValidate(mdlGetProductRequest pRequest)
    {
        var res = ClassFactory.BaseResponseFactory();

        if (res.Header.Messages.Count > 0)
            res.Header.Success = false;
            
        return res;
    }
}