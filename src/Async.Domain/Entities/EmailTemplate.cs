using Async.Domain.Common.Entities;
using Async.Domain.Enum;

namespace Async.Domain.Entities;
public class EmailTemplate : Entity
{
    public NotificationType Type { get; set; }

    public NotificationTemplateType TemplateType { get; set; }

    public string Content { get; set; } = default!;

    public IList<EmailHistory> Histories { get; set; } = new List<EmailHistory>();
    public EmailTemplate()
    {
        Type = NotificationType.Email;
    }

    public string Subject { get; set; } = default!;
}
