using Async.Domain.Entities;
using Async.Domain.Enum;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.EntityConfiguration;
public class NotificationHistoryConfiguration : IEntityTypeConfiguration<NotificationHistory>
{
    public void Configure(EntityTypeBuilder<NotificationHistory> builder)
    {
        builder.Property(template => template.Content).HasMaxLength(129_536);

        builder.ToTable("NotificationHistories")
            .HasDiscriminator(history => history.Type)
            .HasValue<EmailHistory>(NotificationType.Email);

        builder.HasOne<NotificationTemplate>(history => history.Template)
            .WithMany(template => template.Histories)
            .HasForeignKey(history => history.TemplateId);

        builder.HasOne<User>().WithMany().HasForeignKey(history => history.SenderUserId);

        builder.HasOne<User>().WithMany().HasForeignKey(history => history.ReceiverUserId);
    }
}
