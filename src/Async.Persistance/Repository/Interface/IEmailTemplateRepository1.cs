using Async.Domain.Entities;
using System.Linq.Expressions;

namespace Async.Persistance.Repository.Interface;
public interface IEmailTemplateRepository1
{
    ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default);
    IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = null, bool asNoTracking = false);
}