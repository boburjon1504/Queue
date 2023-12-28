using Async.Domain.Common.Entities;
using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Entities;
public class UserSettings:IEntity
{
    public NotificationType? PreferredNotificationType { get; set; }

    /// <summary>
    ///     Gets or sets the user Id
    /// </summary>
    public Guid Id { get; set; }
}
