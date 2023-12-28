using Async.Application.Common.EventBus.Broker;
using Async.Application.Common.Identity.Notififcation.Events;
using Async.Application.Common.Identity.Notififcation.Models;
using Async.Application.Common.Identity.Service;
using Async.Application.Common.Identity.Verification.Service;
using Async.Domain.Constants;
using Async.Domain.Entities;
using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Identity.Service;
public class AccountAggregatorService(IUserService userService,
    IUserSettingsService userSettingsService,
    IUserInfoVerificationCodeService userInfoVerificationCodeService,
    IEventBusBroker eventBusBroker
) : IAccountAggregatorService
{
    public async ValueTask<bool> CreateUserAsync(User user, CancellationToken cancellationToken = default)
    {
        user.Role = RoleType.User;
        var createdUser = await userService.CreateAsync(user, cancellationToken: cancellationToken);
        await userSettingsService.CreateAsync(
            new UserSettings
            {
                Id = createdUser.Id
            },
            cancellationToken: cancellationToken
        );

        // send welcome email
        // var systemUser = await userService.GetSystemUserAsync(cancellationToken: cancellationToken);

        var welcomeNotificationEvent = new ProcessNotificationEvent
        {
            ReceiverUserId = createdUser.Id,
            TemplateType = NotificationTemplateType.WelcomeNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.UserNamePlaceholder, createdUser.FirstName }
            }
        };

        // send verification email
        await eventBusBroker.PublishAsync(
            welcomeNotificationEvent,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName,
            cancellationToken
        );

        var verificationCode = await userInfoVerificationCodeService.CreateAsync(
            VerificationCodeType.EmailAddressVerification,
            createdUser.Id,
            cancellationToken
        );

        // send verification email
        var sendVerificationEvent = new EmailProcessNotificationEvent
        {
            ReceiverUserId = createdUser.Id,
            TemplateType = NotificationTemplateType.EmailAddressVerificationNotification,
            Variables = new Dictionary<string, string>
            {
                { NotificationTemplateConstants.EmailAddressVerificationLinkPlaceholder, verificationCode.VerificationLink }
            }
        };

        await eventBusBroker.PublishAsync(
            sendVerificationEvent,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName,
            cancellationToken
        );

        // await emailOrchestrationService.SendAsync(
        //     new EmailProcessNotificationEvent
        //     {
        //         SenderUserId = systemUser.Id,
        //         ReceiverUserId = createdUser.Id,
        //         TemplateType = NotificationTemplateType.EmailAddressVerificationNotification,
        //         Variables = new Dictionary<string, string>
        //         {
        //             { NotificationTemplateConstants.EmailAddressVerificationLinkPlaceholder, verificationCode.VerificationLink }
        //         }
        //     },
        //     cancellationToken
        // );

        return true;
    }
}

