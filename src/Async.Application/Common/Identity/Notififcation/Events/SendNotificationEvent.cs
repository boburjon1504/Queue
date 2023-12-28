using Async.Application.Common.Identity.Notififcation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Events;
public class SendNotificationEvent:NotificationEvent
{
    public NotificationMessage Message { get; set; } = default!;
}
