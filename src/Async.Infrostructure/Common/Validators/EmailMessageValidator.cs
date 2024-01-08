using Async.Application.Common.Identity.Notififcation.Models;
using Async.Domain.Enum;
using FluentValidation;

namespace Async.Infrostructure.Common.Validators;
public class EmailMessageValidator : AbstractValidator<EmailMessage>
{
    public EmailMessageValidator()
    {
        RuleSet(
            NotificationProcessingEvent.OnRendering.ToString(),
            () =>
            {
                RuleFor(history => history.Template).NotNull();
                RuleFor(history => history.Variables).NotNull();
                RuleFor(history => history.Template.Content).NotNull().NotEmpty();
            }
        );

        RuleSet(
            NotificationProcessingEvent.OnSending.ToString(),
            () =>
            {
                RuleFor(history => history.ReceiverEmailAddress).NotNull().NotEmpty();
                RuleFor(history => history.Subject).NotNull().NotEmpty();
                RuleFor(history => history.Body).NotNull().NotEmpty();
            }
        );
    }
}
