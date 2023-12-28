using Async.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.Repository.Interface;
public interface IUserInfoVerificationCodeRepository
{
    IQueryable<UserInfoVerificationCode> Get(Expression<Func<UserInfoVerificationCode, bool>>? predicate = default, bool asNoTracking = false);

    ValueTask<UserInfoVerificationCode?> GetByIdAsync(Guid codeId, bool asNoTracking = false, CancellationToken cancellationToken = default);

    ValueTask<UserInfoVerificationCode> CreateAsync(
        UserInfoVerificationCode verificationCode,
        bool saveChanges = true,
        CancellationToken cancellationToken = default
    );

    ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true, CancellationToken cancellationToken = default);
}
