using Async.Application.Common.Identity.Notififcation.Models;
using Async.Domain.Common.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Service;
public interface IEmailOrchestrationService
{
    ValueTask<FuncResult<bool>> SendAsync(EmailProcessNotificationEvent @event, CancellationToken cancellationToken = default);
}
