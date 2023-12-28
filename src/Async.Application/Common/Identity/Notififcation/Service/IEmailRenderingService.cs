using Async.Application.Common.Identity.Notififcation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Notififcation.Service;
public interface IEmailRenderingService
{
    ValueTask<string> RenderAsync(
    EmailMessage emailMessage,
    // string template,
    // Dictionary<string, string> variables,
    CancellationToken cancellationToken = default
);
}
