using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Setting;
public class ValidationSettings
{
    public string EmailAddressRegexPattern { get; set; } = default!;

    public string NameRegexPattern { get; set; } = default!;

    public string UrlRegexPattern { get; set; } = default!;
}
