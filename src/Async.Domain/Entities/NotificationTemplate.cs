using Async.Domain.Common.Entities;
using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Entities;
public class NotificationTemplate : Entity
{
    public NotificationType Type { get; set; }

    public NotificationTemplateType TemplateType { get; set; }

    public string Content { get; set; } = default!;

    public IList<NotificationHistory> Histories { get; set; } = new List<NotificationHistory>();
    public Guid Id { get; set; }
}
