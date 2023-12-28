using Async.Application.Common.EventBus.Broker;
using Async.Application.Common.Identity.Notififcation.Events;
using Async.Application.Common.Identity.Notififcation.Models;
using Async.Application.Common.Identity.Notififcation.Service;
using Async.Application.Common.Identity.Serialize;
using Async.Application.Common.Identity.Service;
using Async.Domain.Common.Query;
using Async.Domain.Constants;
using Async.Domain.Entities;
using Async.Domain.Enum;
using Async.Infrostructure.Common.Setting;
using AutoMapper;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using RabbitMQ.Client;
using Async.Domain.Extensions;
using Async.Infrostructure.Common.EventBus.Service;
namespace Async.Infrostructure.Common.Notification.EventSubcriber;
public class NotificationSubscriber(
    IServiceScopeFactory serviceScopeFactory,
    IMapper mapper,
    IJsonSerializationSettingsProvider jsonSerializationSettingsProvider,
    IOptions<NotificationSubscriberSettings> eventBusSubscriberSettings,
    IRabbitMqConnectionProvider rabbitMqConnectionProvider,
    IEventBusBroker eventBusBroker,
    IOptions<NotificationSettings> notificationSettings
)
    : EventSubscriber<NotificationEvent>(
    rabbitMqConnectionProvider,
    eventBusSubscriberSettings,
    [EventBusConstants.ProcessNotificationQueueName, EventBusConstants.RenderNotificationQueueName, EventBusConstants.SendNotificationQueueName],
    jsonSerializationSettingsProvider)

{
    private readonly NotificationSettings _notificationSettings = notificationSettings.Value;

    protected override async ValueTask SetChannelAsync()
    {
        await base.SetChannelAsync();

        await Channel.ExchangeDeclareAsync(EventBusConstants.NotificationExchangeName, ExchangeType.Direct, true);

        await Channel.QueueDeclareAsync(EventBusConstants.ProcessNotificationQueueName, true, false, false);
        await Channel.QueueDeclareAsync(EventBusConstants.RenderNotificationQueueName, true, false, false);
        await Channel.QueueDeclareAsync(EventBusConstants.SendNotificationQueueName, true, false, false);

        await Channel.QueueBindAsync(
            EventBusConstants.ProcessNotificationQueueName,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.ProcessNotificationQueueName
        );

        await Channel.QueueBindAsync(
            EventBusConstants.RenderNotificationQueueName,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.RenderNotificationQueueName
        );

        await Channel.QueueBindAsync(
            EventBusConstants.SendNotificationQueueName,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.SendNotificationQueueName
        );
    }

    protected override async ValueTask<(bool Result, bool Redeliver)> ProcessAsync(NotificationEvent @event, CancellationToken cancellationToken)
    {
        var eventHandler = () => @event switch
        {
            ProcessNotificationEvent processNotificationEvent => ProcessNotificationAsync(processNotificationEvent, cancellationToken),
            RenderNotificationEvent processNotificationEvent => RenderNotificationAsync(processNotificationEvent, cancellationToken),
            SendNotificationEvent processNotificationEvent => SendNotificationAsync(processNotificationEvent, cancellationToken),
            _ => throw new ArgumentOutOfRangeException(nameof(@event))
        };

        // declare exchange and queues
        var result = await eventHandler.GetValueAsync();
        return (result.Data, Redeliver: false);
    }

    private async ValueTask ProcessNotificationAsync(ProcessNotificationEvent processNotificationEvent, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var userService = scope.ServiceProvider.GetRequiredService<IUserService>();
        var emailTemplateService = scope.ServiceProvider.GetRequiredService<IEmailTemplateService>();
        var processNotificationEventValidator = scope.ServiceProvider.GetRequiredService<IValidator<ProcessNotificationEvent>>();

        var validationResult = await processNotificationEventValidator.ValidateAsync(processNotificationEvent, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        var senderUser = processNotificationEvent.SenderUserId != Guid.Empty
            ? await userService.GetByIdAsync(processNotificationEvent.SenderUserId, cancellationToken: cancellationToken)
            : await userService.GetSystemUserAsync(true, cancellationToken);

        // processNotificationEvent.SenderUserId = senderUser!.Id;
        // User_Single_234234
        // User_Collection-234234   

        // User_2342342 - [ User ]
        // User_2342342 - User

        var receiverUserQuery = new QuerySpecification<User>(1, 1, true);
        receiverUserQuery.FilteringOptions.Add(user => user.Id.Equals(processNotificationEvent.ReceiverUserId));
        receiverUserQuery.IncludingOptions.Add(user => user.UserSettings!);

        // userService.Get(user => user.EmailAddress == processNotificationEvent.ReceiverEmailAddress, true);
        var receiverUser = (await userService.GetAsync(receiverUserQuery, cancellationToken)).First();

        // If notification provider type is not specified, get from receiver user settings
        if (!processNotificationEvent.Type.HasValue && receiverUser!.UserSettings!.PreferredNotificationType.HasValue)
            processNotificationEvent.Type = receiverUser!.UserSettings.PreferredNotificationType!.Value;

        // If user not specified preferred notification type get from settings
        if (!processNotificationEvent.Type.HasValue)
            processNotificationEvent.Type = _notificationSettings.DefaultNotificationType;

        var renderNotificationEvent = new RenderNotificationEvent
        {
            SenderUserId = senderUser!.Id,
            ReceiverUserId = receiverUser.Id,
            Template = (await emailTemplateService.GetByTypeAsync(processNotificationEvent.TemplateType, cancellationToken: cancellationToken))!,
            SenderUser = senderUser,
            ReceiverUser = receiverUser,
            Variables = processNotificationEvent.Variables ?? new Dictionary<string, string>()
        };

        await eventBusBroker.PublishAsync(
            renderNotificationEvent,
            EventBusConstants.NotificationExchangeName,
            EventBusConstants.RenderNotificationQueueName,
            cancellationToken
        );
    }

    private async ValueTask RenderNotificationAsync(RenderNotificationEvent renderNotificationEvent, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var emailRenderingService = scope.ServiceProvider.GetRequiredService<IEmailRenderingService>();

        if (renderNotificationEvent.Template.Type == NotificationType.Email)
        {
            var emailMessage = new EmailMessage
            {
                SenderEmailAddress = renderNotificationEvent.SenderUser.EmailAddress,
                ReceiverEmailAddress = renderNotificationEvent.ReceiverUser.EmailAddress,
                EmailTemplate = (EmailTemplate)renderNotificationEvent.Template,
                Variables = renderNotificationEvent.Variables
            };

            await emailRenderingService.RenderAsync(emailMessage, cancellationToken);

            var sendNotificationEvent = new SendNotificationEvent
            {
                SenderUserId = renderNotificationEvent.SenderUserId,
                ReceiverUserId = renderNotificationEvent.ReceiverUserId,
                Message = emailMessage
            };

            await eventBusBroker.PublishAsync(
                sendNotificationEvent,
                EventBusConstants.NotificationExchangeName,
                EventBusConstants.SendNotificationQueueName,
                cancellationToken
            );
        }
    }

    private async ValueTask SendNotificationAsync(SendNotificationEvent sendNotificationEvent, CancellationToken cancellationToken)
    {
        await using var scope = serviceScopeFactory.CreateAsyncScope();
        var emailSenderService = scope.ServiceProvider.GetRequiredService<IEmailSenderService>();
        var emailHistoryService = scope.ServiceProvider.GetRequiredService<IEmailHistoryService>();

        if (sendNotificationEvent.Message is EmailMessage emailMessage)
        {
            await emailSenderService.SendAsync(emailMessage, cancellationToken);

            var history = mapper.Map<EmailHistory>(emailMessage);
            history.SenderUserId = sendNotificationEvent.SenderUserId;
            history.ReceiverUserId = sendNotificationEvent.ReceiverUserId;

            await emailHistoryService.CreateAsync(history, cancellationToken: cancellationToken);

            if (!history.IsSuccessful) throw new InvalidOperationException("Email history is not created");
        }
    }
}
