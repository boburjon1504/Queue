using Async.Application.Common.Identity.Notififcation.Service;
using Async.Domain.Common.Query;
using Async.Domain.Entities;
using Async.Domain.Enum;
using Async.Persistance.Extension;
using Async.Persistance.Repository.Interface;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Notification.Service;
public class EmailTemplateService : IEmailTemplateService
{
    private readonly IEmailTemplateRepository _emailTemplateRepository;
    private readonly IValidator<EmailTemplate> _emailTemplateValidator;

    public EmailTemplateService(IEmailTemplateRepository emailTemplateRepository, IValidator<EmailTemplate> emailTemplateValidator)
    {
        _emailTemplateRepository = emailTemplateRepository;
        _emailTemplateValidator = emailTemplateValidator;
    }

    public ValueTask<EmailTemplate> CreateAsync(EmailTemplate emailTemplate, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var validationResult = _emailTemplateValidator.Validate(emailTemplate);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);

        return _emailTemplateRepository.CreateAsync(emailTemplate, saveChanges, cancellationToken);
    }

    public async ValueTask<IList<EmailTemplate>> GetByFilterAsync(FilterPagination paginationOptions, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await Get(asNoTracking: asNoTracking).ApplyPagination(paginationOptions).ToListAsync(cancellationToken);
    }

    public async ValueTask<EmailTemplate?> GetByTypeAsync(NotificationTemplateType templateType, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await _emailTemplateRepository.Get(template => template.TemplateType == templateType, asNoTracking)
       .SingleOrDefaultAsync(cancellationToken);
    }
    public IQueryable<EmailTemplate> Get(Expression<Func<EmailTemplate, bool>>? predicate = default, bool asNoTracking = false)
    {
        return _emailTemplateRepository.Get(predicate, asNoTracking);
    }
}
