using Async.Application.Common.Identity.Notififcation.Events;
using Async.Application.Common.Identity.Notififcation.Models;
using Async.Domain.Common.Exeptions;
using Async.Domain.Entities;

namespace Async.Application.Common.Identity.Notififcation.Service;
public interface INotificationAggregatorService
{
    ValueTask<FuncResult<bool>> SendAsync(ProcessNotificationEvent processNotificationEvent, CancellationToken cancellationToken = default);

    ValueTask<IList<EmailTemplate>> GetTemplatesByFilterAsync(
        NotificationTemplateFilter filter,
        CancellationToken cancellationToken = default
    );
}
