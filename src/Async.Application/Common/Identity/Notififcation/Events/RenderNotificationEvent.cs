using Async.Domain.Entities;

namespace Async.Application.Common.Identity.Notififcation.Events;
public class RenderNotificationEvent:NotificationEvent
{
    public EmailTemplate Template { get; set; } = default!;

    public User SenderUser { get; init; } = default!;

    public User ReceiverUser { get; init; } = default!;

    public Dictionary<string, string> Variables { get; set; } = new();
}
