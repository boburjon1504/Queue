using Async.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.EntityConfiguration;
public class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.Property(user => user.FirstName).IsRequired().HasMaxLength(64);
        builder.Property(user => user.LastName).IsRequired().HasMaxLength(64);
        builder.Property(user => user.EmailAddress).IsRequired().HasMaxLength(64);
        builder.Property(user => user.EmailAddress).IsRequired().HasMaxLength(64);
        builder.Property(user => user.PasswordHash).IsRequired().HasMaxLength(256);

        builder.HasIndex(user => user.EmailAddress).IsUnique();
    }
}
