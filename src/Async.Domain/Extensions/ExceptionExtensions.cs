using Async.Domain.Common.Exeptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Extensions;
public static class ExceptionExtensions
{
    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<Task<T>> predicate)
    {
        try
        {
            return new FuncResult<T>(await predicate());
        }catch (Exception ex)
        {
            return new FuncResult<T>(ex);
        }
    }
    public static async ValueTask<FuncResult<T>> GetValueAsync<T>(this Func<ValueTask<T>> predicate)
    {
        try
        {
            return new FuncResult<T>(await predicate());
        }
        catch (Exception ex)
        {
            return new FuncResult<T>(ex);
        }
    }
    public static async ValueTask<FuncResult<bool>> GetValueAsync(this Func<ValueTask> predicate)
    {
        try
        {
            await predicate();
            return new FuncResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new FuncResult<bool>(ex);
        }
    }
    public static async ValueTask<FuncResult<bool>> GetValueAsync(this Func<Task> predicate)
    {
        try
        {
            await predicate();
            return new FuncResult<bool>(true);
        }
        catch (Exception ex)
        {
            return new FuncResult<bool>(ex);
        }
    }
}
