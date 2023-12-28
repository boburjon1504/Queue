using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Application.Common.Identity.Serialize;
public interface IJsonSerializationSettingsProvider
{
    JsonSerializerSettings Get(bool clone = false);
}
