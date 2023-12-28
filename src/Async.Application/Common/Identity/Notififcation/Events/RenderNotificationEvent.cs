using Async.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Events;
public class RenderNotificationEvent:NotificationEvent
{
    public NotificationTemplate Template { get; set; } = default!;

    public User SenderUser { get; init; } = default!;

    public User ReceiverUser { get; init; } = default!;

    public Dictionary<string, string> Variables { get; set; } = new();
}
