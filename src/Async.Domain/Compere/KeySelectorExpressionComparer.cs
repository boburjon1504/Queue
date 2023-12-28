using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Compere;
public class KeySelectorExpressionComparer<TSource> : IComparer<Expression<Func<TSource, object>>>
{
    public int Compare(Expression<Func<TSource, object>>? x, Expression<Func<TSource, object>>? y)
    {
        if (ReferenceEquals(x, y)) return 0;
        if (ReferenceEquals(null, y)) return 1;
        if (ReferenceEquals(null, x)) return -1;

        return string.Compare(x.ToString(), y.ToString(), StringComparison.Ordinal);
    }
}
