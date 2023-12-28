using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Setting;
public class TemplateRenderingSettings
{
    public string PlaceholderRegexPattern { get; set; } = default!;

    public string PlaceholderValueRegexPattern { get; set; } = default!;

    public int RegexMatchTimeoutInSeconds { get; set; }
}
