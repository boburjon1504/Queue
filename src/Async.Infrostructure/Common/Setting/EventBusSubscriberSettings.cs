using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Setting;
public abstract class EventBusSubscriberSettings
{
    public ushort PrefetchCount { get; set; }
}
