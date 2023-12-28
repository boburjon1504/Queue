using Async.Application.Common.Identity.Notififcation.Events;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Validators;
public class ProcessNotificationEventValidator : AbstractValidator<ProcessNotificationEvent>
{
    public ProcessNotificationEventValidator()
    {
        RuleFor(history => history.ReceiverUserId).NotEqual(Guid.Empty);
    }
}
