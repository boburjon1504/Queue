using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Common.Cashing;
public interface ICacheModel
{
    /// <summary>
    ///     Gets computed cache key.
    /// </summary>
    string CacheKey { get; }
}
