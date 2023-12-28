using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Events;
public class ProcessNotificationEvent:NotificationEvent
{
    public NotificationTemplateType TemplateType { get; init; }

    public NotificationType? Type { get; set; }

    public Dictionary<string, string>? Variables { get; set; }
}
