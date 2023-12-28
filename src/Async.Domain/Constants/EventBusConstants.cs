using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Constants;
public class EventBusConstants
{
    public const string NotificationExchangeName = "Notifications";
    public const string ProcessNotificationQueueName = "ProcessNotification";
    public const string RenderNotificationQueueName = "RenderNotification";
    public const string SendNotificationQueueName = "SendNotification";
}
