using Async.Application.Common.Identity.Notififcation.Events;
using Async.Application.Common.Identity.Notififcation.Models;
using Async.Domain.Common.Exeptions;
using Async.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Service;
public interface INotificationAggregatorService
{
    ValueTask<FuncResult<bool>> SendAsync(ProcessNotificationEvent processNotificationEvent, CancellationToken cancellationToken = default);

    ValueTask<IList<NotificationTemplate>> GetTemplatesByFilterAsync(
        NotificationTemplateFilter filter,
        CancellationToken cancellationToken = default
    );
}
