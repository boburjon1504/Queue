using Async.Domain.Common.Query;
using Async.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Service;
public interface IEmailHistoryService
{
    ValueTask<IList<EmailHistory>> GetByFilterAsync(
      FilterPagination paginationOptions,
      bool asNoTracking = false,
      CancellationToken cancellationToken = default
  );

    ValueTask<EmailHistory> CreateAsync(EmailHistory emailHistory, bool saveChanges = true, CancellationToken cancellationToken = default);
}
