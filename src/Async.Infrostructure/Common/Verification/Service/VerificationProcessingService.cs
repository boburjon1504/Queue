using Async.Application.Common.Identity.Service;
using Async.Application.Common.Identity.Verification.Service;
using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Verification.Service;
public class VerificationProcessingService(IUserInfoVerificationCodeService userInfoVerificationCodeService, IUserService userService)
    : IVerificationProcessingService
{
    public async ValueTask<bool> Verify(string code, CancellationToken cancellationToken)
    {
        var codeType = await userInfoVerificationCodeService.GetVerificationTypeAsync(code, cancellationToken) ??
                throw new InvalidOperationException("Verification code is not found.");

        var result = codeType switch
        {
            VerificationType.UserInfoVerificationCode => VerifyUserInfoAsync(code, cancellationToken),
            _ => throw new NotSupportedException("Verification type is not supported.")
        };

        return await result;
    }
    private async ValueTask<bool> VerifyUserInfoAsync(string code, CancellationToken cancellationToken = default)
    {
        var userInfoVerificationCode = await userInfoVerificationCodeService.GetByCodeAsync(code, cancellationToken);

        if (!userInfoVerificationCode.IsValid) return false;

        var user = await userService.GetByIdAsync(userInfoVerificationCode.Code.UserId, cancellationToken: cancellationToken) ??
                   throw new InvalidOperationException();

        switch (userInfoVerificationCode.Code.CodeType)
        {
            case VerificationCodeType.EmailAddressVerification:
                user.IsEmailAddressVerified = true;
                await userService.UpdateAsync(user, false, cancellationToken);
                break;
            default: throw new NotSupportedException();
        }

        await userInfoVerificationCodeService.DeactivateAsync(userInfoVerificationCode.Code.Id, cancellationToken: cancellationToken);

        return true;
    }
}
