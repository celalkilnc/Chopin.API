using MusicStore.Application.Models.Request;
using MusicStore.Application.Models.Response;
using MusicStore.Application.Utils;
using MusicStore.Persistance.Repositories.User;

namespace MusicStore.Application.Validations;

public class AuthValidator
{
    public static BaseResponse AuthValidate(mdlAuthenticationRequest pRequest,IUserRepository userRepository, IConfiguration configuration)
    {
        var res = ClassFactory.BaseResponseFactory(true);

        if(!FormatChecker.IsValidEmail(pRequest.Email))
            res.Header.Messages.Add(ResponseMessages.UnvalidEmail);

        var user = userRepository.GetWhere(x => x.Email == pRequest.Email).FirstOrDefault();

        if (user == null || Encrypter.EncryptAES(user.Password, configuration["Encrypt:Key"],
                configuration["Encrypt:Iv"]) != pRequest.Password)
        {
            res.Header.Messages.Add(ResponseMessages.UnvalidLogin);
            return res;
        }
        
        
        if (res.Header.Messages.Count > 0)
            res.Header.Success = false;

        return res;
    }

    public static BaseResponse CustomerRegisterValidate(mdlCustomerRegisterRequest pRequest,IUserRepository userRepository)
    {
        var res = ClassFactory.BaseResponseFactory(true);

        if(!FormatChecker.IsValidEmail(pRequest.Email))
            res.Header.Messages.Add(ResponseMessages.UnvalidEmail);
        
        if (pRequest.Password != pRequest.PasswordCheck)
            res.Header.Messages.Add(ResponseMessages.UnmatchedPasswords);

        if(userRepository.GetWhere(x => x.Email == pRequest.Email).FirstOrDefault() != null)
            res.Header.Messages.Add(ResponseMessages.TakenEmail);
        
        if (res.Header.Messages.Count > 0)
            res.Header.Success = false;

        return res;
    }
}