using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Verification.Service;
public interface IVerificationProcessingService
{
    ValueTask<bool> Verify(string code, CancellationToken cancellationToken);
}
