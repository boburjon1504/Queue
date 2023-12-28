using Async.Domain.Common.Query;
using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Models;
public class NotificationTemplateFilter:FilterPagination
{
    public IList<NotificationType> TemplateType { get; set; }
}
