using Async.Domain.Entities;
using Async.Domain.Enum;
using Async.Infrostructure.Common.Setting;
using FluentValidation;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Validators;
public class UserInfoVerificationCodeValidator : AbstractValidator<UserInfoVerificationCode>
{
    public UserInfoVerificationCodeValidator(IOptions<VerificationSettings> verificationSettings, IOptions<ValidationSettings> validationSettings)
    {
        var verificationSettingsValue = verificationSettings.Value;
        var validationSettingsValue = validationSettings.Value;

        RuleSet(
            EntityEvent.OnCreate.ToString(),
            () =>
            {
                RuleFor(code => code.UserId).NotEqual(Guid.Empty);

                RuleFor(code => code.ExpiryTime)
                    .GreaterThanOrEqualTo(DateTimeOffset.UtcNow)
                    .LessThanOrEqualTo(DateTimeOffset.UtcNow.AddSeconds(verificationSettingsValue.VerificationCodeExpiryTimeInSeconds));

                RuleFor(code => code.IsActive).Equal(true);

                RuleFor(code => code.VerificationLink).NotEmpty().Matches(validationSettingsValue.UrlRegexPattern);
            }
        );
    }
}
