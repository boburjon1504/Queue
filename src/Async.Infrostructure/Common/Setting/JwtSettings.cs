using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Setting;
public class JwtSettings
{
    public bool ValidateIssuer { get; set; }

    public string ValidIssuer { get; set; } = default!;

    public bool ValidateAudience { get; set; }

    public string ValidAudience { get; set; } = default!;

    public bool ValidateLifetime { get; set; }

    public int ExpirationTimeInMinutes { get; set; }

    public bool ValidateIssuerSigningKey { get; set; }

    public string SecretKey { get; set; } = default!;
}
