using Async.Domain.Common.Entities;
using Async.Domain.Enum;
using System.ComponentModel.DataAnnotations.Schema;

namespace Async.Domain.Entities;
public class EmailHistory:IEntity
{
    public Guid TemplateId { get; set; }

    public Guid SenderUserId { get; set; }

    public Guid ReceiverUserId { get; set; }

    public NotificationType Type { get; set; }

    public string Content { get; set; } = default!;

    public bool IsSuccessful { get; set; }

    public string? ErrorMessage { get; set; }

    public EmailTemplate Template { get; set; }
    public Guid Id { get; set; }

    public EmailHistory()
    {
        Type = NotificationType.Email;
    }

    public string SenderEmailAddress { get; set; } = default!;

    public string ReceiverEmailAddress { get; set; } = default!;

    public string Subject { get; set; } = default!;

    [NotMapped]
    public EmailTemplate EmailTemplate
    {
        get => Template is not null ? Template as EmailTemplate : null;
        set => Template = value;
    }
}
