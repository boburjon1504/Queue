using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Common.Query;
public class FilterPagination
{
    /// <summary>
    ///     Gets the size of the page ( limit of items in query result )
    /// </summary>
    public uint PageSize { get; init; }

    /// <summary>
    ///     Gets the page token ( identifier ) of query
    /// </summary>
    public uint PageToken { get; init; }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();

        hashCode.Add(PageSize);
        hashCode.Add(PageToken);

        return hashCode.ToHashCode();
    }

    public override bool Equals(object? obj)
    {
        return obj is FilterPagination filterPagination && filterPagination.GetHashCode() == GetHashCode();
    }
}
