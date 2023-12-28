using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Entities;
public class EmailTemplate:NotificationTemplate
{
    public EmailTemplate()
    {
        Type = NotificationType.Email;
    }

    public string Subject { get; set; } = default!;
}
