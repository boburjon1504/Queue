using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Common.Cashing;
public class CacheEntryOptions
{
    public CacheEntryOptions()
    {
    }

    public CacheEntryOptions(TimeSpan? absoluteExpirationRelativeToNow, TimeSpan? slidingExpiration)
    {
        AbsoluteExpirationRelativeToNow = absoluteExpirationRelativeToNow;
        SlidingExpiration = slidingExpiration;
    }

    public TimeSpan? AbsoluteExpirationRelativeToNow { get; init; }
    public TimeSpan? SlidingExpiration { get; init; }
}
