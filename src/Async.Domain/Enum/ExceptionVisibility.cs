using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Enum;
public enum ExceptionVisibility
{
    /// <summary>
    ///     Indicates that the exception can be seen by client and users
    /// </summary>
    Public,

    /// <summary>
    ///     Indicates that the exception must not leave the system and can be logged
    /// </summary>
    Protected,

    /// <summary>
    ///     Indicates that the exception must not leave the system and can be logged
    /// </summary>
    Private
}
