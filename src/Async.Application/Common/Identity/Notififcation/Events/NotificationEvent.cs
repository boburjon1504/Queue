using Async.Domain.Common.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Events;
public class NotificationEvent:Event
{
    public Guid SenderUserId { get; init; }

    public Guid ReceiverUserId { get; init; }
}
