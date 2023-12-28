using Async.Domain.Entities;
using Async.Persistance.Cashing.Broker;
using Async.Persistance.DataContext;
using Async.Persistance.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.Repository;
public class EmailHistoryRepository(NotificationDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<EmailHistory, NotificationDbContext>(dbContext, cacheBroker), IEmailHistoryRepository
{
    public async ValueTask<EmailHistory> CreateAsync(EmailHistory emailHistory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        if (emailHistory.EmailTemplate is not null)
            DbContext.Entry(emailHistory.EmailTemplate).State = EntityState.Unchanged;

        var createdHistory = await base.CreateAsync(emailHistory, saveChanges, cancellationToken);

        if (emailHistory.EmailTemplate is not null)
            DbContext.Entry(emailHistory.EmailTemplate).State = EntityState.Detached;

        return createdHistory;
    }

    public IQueryable<EmailHistory> Get(Expression<Func<EmailHistory, bool>>? predicate = null, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }
}
