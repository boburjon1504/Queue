﻿using Async.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.EntityConfiguration;
public class EmailHistoryConfiguration : IEntityTypeConfiguration<EmailHistory>
{
    public void Configure(EntityTypeBuilder<EmailHistory> builder)
    {
        builder.Property(template => template.SenderEmailAddress).IsRequired().HasMaxLength(256);
        builder.Property(template => template.ReceiverEmailAddress).IsRequired().HasMaxLength(256);
        builder.Property(template => template.Subject).IsRequired().HasMaxLength(256);
    }
}


