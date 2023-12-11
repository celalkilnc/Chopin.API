using MusicStore.API.Application.Models.Request.Authorization;
using MusicStore.API.Application.Models.Response;
using MusicStore.API.Application.Models.Response.Authorization;
using MusicStore.API.Domain.Entities;
using MusicStore.API.Domain.Enumerations;
using MusicStore.API.Persistance.Repositories;
using MusicStore.API.Utils;
using MusicStore.API.Utils.Authorization;

namespace MusicStore.API.Actions;

public class UserAction : IUserService
{
    public async Task<BaseResponse> Register(mdlRegisterRequest pRequest, IUserRepository userRepository)
    {
        var res = new BaseResponse() { Header = new() { Messages = new(), Success = false } };
        bool isOk = true;

        #region ..:: Validations ::..

        if (!FormatChecker.IsValidEmail(pRequest.Email))
        {
            res.Header.Messages.Add(ResponseMessages.UnvalidEmail);
            isOk = false;
        }

        if (pRequest.Password != pRequest.ScndPassword)
        {
            res.Header.Messages.Add(ResponseMessages.UnmatchedPassword);
            isOk = false;
        }

        #endregion

        if (isOk)
        {
            var isSuccess = await userRepository.AddAsync(new()
            {
                Id = Guid.NewGuid(),
                Email = pRequest.Email,
                Status = enmUserStatus.Customer,
                Name = pRequest.UserName,
                PhoneNumber = string.IsNullOrEmpty(pRequest.PhoneNumber) ? "" : pRequest.PhoneNumber,
                Password = EnryptOperations.EncryptAES(pRequest.Password)
            });

            res.Header.Messages.Add(isSuccess ? ResponseMessages.Success : ResponseMessages.Failed);
            res.Header.Success = isSuccess;
            await userRepository.SaveAsync();
        }

        res.Header.RequestDate = DateTime.Now;
        return res;
    }

    public async Task<mdlAuthenticationResponse> Authentication(mdlAuthenticationRequest pRequest,
        IUserRepository userRepository)
    {
        #region ..::Definations::..

        var isOk = true;
        User user = new();
        var res = new mdlAuthenticationResponse()
            { Body = new(),Header = new() { Messages = new(), RequestDate = DateTime.Now, Success = false } };

        #endregion
        
        #region ..::Validations::..

        if (!FormatChecker.IsValidEmail(pRequest.Email))
        {
            res.Header.Messages.Add(ResponseMessages.UnvalidEmail);
            isOk = false;
        }

        if (isOk)
            user = userRepository.GetWhere(x => x.Email == pRequest.Email).FirstOrDefault();

        #endregion

        if (isOk && (user != null))
        {
            if (user.Password == EnryptOperations.EncryptAES(pRequest.Password))
            {
                var tokenService = new TokenService();
                res.Body.Token = tokenService.GenerateToken(user.Name, Helper.GetEnumDescription(user.Status));
                res.Body.Expression = DateTime.Now.AddHours(5);
                res.Body.AuthorityLevel = user.Status;
                res.Header.Messages.Add(ResponseMessages.Success);
                res.Header.Success = true;
            }
            else
                res.Header.Messages.Add(ResponseMessages.UnmatchedCredentials);
            
        } 
        else if (isOk && user == null)
            res.Header.Messages.Add(ResponseMessages.UnmatchedCredentials);
        
        return res;
    }
}