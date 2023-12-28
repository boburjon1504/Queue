using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Setting;
public class PasswordValidationSettings
{
    public bool RequireDigit { get; set; }
    public bool RequireUppercase { get; set; }
    public bool RequireLowercase { get; set; }
    public bool RequireNonAlphanumeric { get; set; }
    public int MinimumLength { get; set; }
    public int MaximumLength { get; set; }
}

