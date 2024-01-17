using Microsoft.IdentityModel.Tokens;
using MusicStore.API.Services;
using MusicStore.Application.Models.Request;
using MusicStore.Application.Models.Response;
using MusicStore.Application.Models.Response.Auth;
using MusicStore.Application.Utils;
using MusicStore.Application.Utils.AppSetting;
using MusicStore.Application.Validations;
using MusicStore.Domain.Enumerations;
using MusicStore.Persistance.Repositories.User;

namespace MusicStore.API.Actions;

public class AuthAction : IAuthService
{
    private IConfiguration _configuration;

    public AuthAction(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<mdlAuthResponse> Authentication(mdlAuthenticationRequest pRequest,
        IUserRepository userRepository)
    {
        var validRes = AuthValidator.AuthValidate(pRequest, userRepository, _configuration);
        var res = new mdlAuthResponse().Factory();

        if (!validRes.Header.Success)
        {
            res.Header.Messages = validRes.Header.Messages;
            res.Header.Success = validRes.Header.Success;
            return res;
        }

        try
        {
            var key = _configuration["JWTSettings:SecretKey"];
            var user = userRepository.GetWhere(x => x.Email == pRequest.Email).FirstOrDefault();
            var tokenRes = TokenService.GenerateToken(user, key, key);

            res.Body.Token = tokenRes.Token;
            res.Body.UserStatus = tokenRes.Status;
            res.Body.Expire = tokenRes.Expire;
            res.Header.Messages.Add(ResponseMessages.Successfuly);
            res.Header.Success = true;
        }
        catch (Exception e)
        {
            res.Header.Messages.Add(e.ToString());
            res.Header.Success = false;
        }

        return res;
    }

    public async Task<BaseResponse> CustomerRegister(mdlRegisterRequest pRequest,
        IUserRepository userRepository)
    {
        return await Register(pRequest, userRepository, enmUserStatus.Customer);
    }

    public async Task<BaseResponse> AdminRegister(mdlRegisterRequest pRequest, IUserRepository? userRepository)
    {
        return await Register(pRequest, userRepository, enmUserStatus.Admin);
    }

    private async Task<BaseResponse> Register(mdlRegisterRequest pRequest,
        IUserRepository userRepository, enmUserStatus role)
    {
        var validRes = AuthValidator.RegisterValidate(pRequest, userRepository);

        if (!validRes.Header.Success)
            return validRes;

        var isSuccess = await userRepository.AddAsync(new()
        {
            Email = pRequest.Email,
            Name = pRequest.UserName,
            PhoneNumber = string.IsNullOrEmpty(pRequest.PhoneNumber) ? "0" : pRequest.PhoneNumber,
            Password = Encrypter.EncryptAES(pRequest.Password, AppSetting.PassKey,
                AppSetting.PassIv),
            Status = role,
            ID = Guid.NewGuid()
        });
        await userRepository.SaveAsync();

        return ClassFactory.BaseResponseFactory(
            new() { isSuccess ? ResponseMessages.Successfuly : ResponseMessages.Failed }, isSuccess);
    }
}