using Async.Domain.Common.Query;
using Async.Domain.Entities;
using Async.Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Service;
public interface IEmailTemplateService
{
    ValueTask<IList<EmailTemplate>> GetByFilterAsync(
    FilterPagination paginationOptions,
    bool asNoTracking = false,
    CancellationToken cancellationToken = default
);

    ValueTask<EmailTemplate?> GetByTypeAsync(
        NotificationTemplateType templateType,
        bool asNoTracking = false,
        CancellationToken cancellationToken = default
    );

    ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
}
