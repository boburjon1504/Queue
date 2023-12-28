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
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Notification.Service;
public class EmailHistoryService : IEmailHistoryService
{
    private readonly IEmailHistoryRepository _emailHistoryRepository;
    private readonly IValidator<EmailHistory> _emailHistoryValidator;

    public EmailHistoryService(IEmailHistoryRepository emailHistoryRepository, IValidator<EmailHistory> emailHistoryValidator)
    {
        _emailHistoryRepository = emailHistoryRepository;
        _emailHistoryValidator = emailHistoryValidator;
    }
    public async ValueTask<EmailHistory> CreateAsync(EmailHistory emailHistory, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        var validationResult = await _emailHistoryValidator.ValidateAsync(
            emailHistory,
            options => options.IncludeRuleSets(EntityEvent.OnCreate.ToString()),
            cancellationToken
        );
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        return await _emailHistoryRepository.CreateAsync(emailHistory, saveChanges, cancellationToken);
    }

    public async ValueTask<IList<EmailHistory>> GetByFilterAsync(FilterPagination paginationOptions, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await _emailHistoryRepository.Get().ApplyPagination(paginationOptions).ToListAsync(cancellationToken);
    }
}
