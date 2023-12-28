using Async.Domain.Entities;
using Async.Persistance.Cashing.Broker;
using Async.Persistance.DataContext;
using Async.Persistance.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.Repository;
public class EmailTemplateRepository(NotificationDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<EmailTemplate, NotificationDbContext>(dbContext, cacheBroker), IEmailTemplateRepository
{

    public ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(emailTemplate, saveChanges, cancellationToken);
    }

    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = null, bool asNoTracking = false)
    {
        return base.Get(predicate, asNoTracking);
    }
}
