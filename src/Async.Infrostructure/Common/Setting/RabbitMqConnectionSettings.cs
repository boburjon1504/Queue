using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Setting;
public class RabbitMqConnectionSettings
{
    public string HostName { get; set; } = default!;

    public int Port { get; set; }
}
