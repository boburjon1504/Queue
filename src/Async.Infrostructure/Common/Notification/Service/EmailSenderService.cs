using Async.Application.Common.Identity.Notififcation.Broker;
using Async.Application.Common.Identity.Notififcation.Models;
using Async.Application.Common.Identity.Notififcation.Service;
using Async.Domain.Enum;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Async.Domain.Extensions;
namespace Async.Infrostructure.Common.Notification.Service;
public class EmailSenderService : IEmailSenderService
{
    private readonly IValidator<EmailMessage> _emailMessageValidator;
    private readonly IEnumerable<IEmailSenderBroker> _emailSenderBrokers;

    public  EmailSenderService(IEnumerable<IEmailSenderBroker> emailSenderBrokers, IValidator<EmailMessage> emailMessageValidator)
    {
        _emailSenderBrokers = emailSenderBrokers;
        _emailMessageValidator = emailMessageValidator;
    }
    public async ValueTask<bool> SendAsync(EmailMessage emailMessage, CancellationToken cancellationToken = default)
    {
        var validationResult = _emailMessageValidator.Validate(
      emailMessage,
      options => options.IncludeRuleSets(NotificationProcessingEvent.OnSending.ToString())
  );
        if (!validationResult.IsValid) throw new ValidationException(validationResult.Errors);

        foreach (var emailSenderBroker in _emailSenderBrokers)
        {
            var sendNotificationTask = () => emailSenderBroker.SendAsync(emailMessage, cancellationToken);
            var result = await sendNotificationTask.GetValueAsync();

            emailMessage.IsSuccessful = result.IsSuccess;
            emailMessage.ErrorMessage = result.Exception?.Message;
            return result.IsSuccess;
        }

        return false;
    }
}
