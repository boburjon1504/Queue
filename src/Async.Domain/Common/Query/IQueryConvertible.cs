using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Common.Query;
public interface IQueryConvertible<TSourse>
{
    /// <summary>
    ///     Converts to query specification.
    /// </summary>
    /// <returns>Query specification</returns>
    QuerySpecification<TSourse> ToQuerySpecification();
}
