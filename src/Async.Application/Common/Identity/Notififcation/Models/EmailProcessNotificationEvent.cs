using Async.Application.Common.Identity.Notififcation.Events;
using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Models;
public class EmailProcessNotificationEvent:ProcessNotificationEvent
{
    public EmailProcessNotificationEvent()
    {
        Type = NotificationType.Email;
    }
}
