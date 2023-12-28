using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Setting;
public class VerificationSettings
{
    public string VerificationLink { get; set; } = default!;

    public int VerificationCodeExpiryTimeInSeconds { get; set; }

    public int VerificationCodeLength { get; set; }
}
