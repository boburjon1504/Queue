using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Common.Exeptions;
public class FuncResult<T>
{
    public FuncResult(T data)
    {
        Data = data;
    }

    public FuncResult(Exception exception)
    {
        Exception = exception;
    }

    public T Data { get; init; }

    public Exception? Exception { get; }

    public bool IsSuccess => Exception is null;
}
