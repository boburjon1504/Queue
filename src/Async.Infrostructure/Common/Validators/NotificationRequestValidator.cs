using Async.Application.Common.Identity.Notififcation.Events;
using Async.Application.Common.Identity.Service;
using Async.Domain.Enum;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Validators;
public class NotificationRequestValidator : AbstractValidator<ProcessNotificationEvent>
{
    public NotificationRequestValidator(IUserService userService)
    {
        // TODO : to external
        var templatesRequireSender = new List<NotificationTemplateType>
        {
            NotificationTemplateType.ReferralNotification
        };

        RuleFor(request => request.SenderUserId)
            .NotEqual(Guid.Empty)
            .NotNull()
            .When(request => templatesRequireSender.Contains(request.TemplateType))
            .CustomAsync(
                async (senderUserId, context, cancellationToken) =>
                {
                    var user = await userService.GetByIdAsync(senderUserId, true, cancellationToken);

                    if (user is null)
                        context.AddFailure("Sender user not found");
                }
            );

        RuleFor(request => request.ReceiverUserId)
            .NotEqual(Guid.Empty)
            .CustomAsync(
                async (senderUserId, context, cancellationToken) =>
                {
                    var user = await userService.GetByIdAsync(senderUserId, true, cancellationToken);

                    if (user is null)
                        context.AddFailure("Sender user not found");
                }
            );
    }
}
