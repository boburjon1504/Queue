using Async.Domain.Entities;
using Async.Persistance.Cashing.Broker;
using Async.Persistance.DataContext;
using Async.Persistance.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.Repository;
public class UserInfoVerificationCodeRepository(NotificationDbContext identityDbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<UserInfoVerificationCode, NotificationDbContext>(identityDbContext, cacheBroker), IUserInfoVerificationCodeRepository
{
    public async ValueTask<UserInfoVerificationCode> CreateAsync(UserInfoVerificationCode verificationCode, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        await DbContext.UserInfoVerificationCodes.Where(code => code.UserId == verificationCode.UserId && code.CodeType == verificationCode.CodeType)
         .ExecuteUpdateAsync(setter => setter.SetProperty(code => code.IsActive, false), cancellationToken);

        return await base.CreateAsync(verificationCode, saveChanges, cancellationToken); ;
    }

    public ValueTask DeactivateAsync(Guid codeId, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public ValueTask<UserInfoVerificationCode?> GetByIdAsync(Guid codeId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    IQueryable<UserInfoVerificationCode> IUserInfoVerificationCodeRepository.Get(Expression<Func<UserInfoVerificationCode, bool>>? predicate, bool asNoTracking)
    {
        throw new NotImplementedException();
    }
}
