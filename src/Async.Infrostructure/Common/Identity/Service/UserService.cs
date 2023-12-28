using Async.Application.Common.Identity.Service;
using Async.Domain.Common.Query;
using Async.Domain.Entities;
using Async.Domain.Enum;
using Async.Persistance.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Identity.Service;
public class UserService(IUserRepository userRepository) : IUserService
{
    public ValueTask<User> CreateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return userRepository.CreateAsync(user, saveChanges, cancellationToken);
    }

    public IQueryable<User> Get(Expression<Func<User, bool>>? predicate = null, bool asNoTracking = false)
    {
        return userRepository.Get(predicate, asNoTracking);
    }

    public ValueTask<IList<User>> GetAsync(QuerySpecification<User> querySpecification, CancellationToken cancellationToken = default)
    {
        return userRepository.GetAsync(querySpecification, cancellationToken);
    }

    public ValueTask<User?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return userRepository.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }

    public async Task<Guid?> GetIdByEmailAddressAsync(string emailAddress, CancellationToken cancellationToken = default)
    {
        var userId = await Get(user => user.EmailAddress == emailAddress, true).Select(user => user.Id).FirstOrDefaultAsync(cancellationToken);
        return userId != Guid.Empty ? userId : default(Guid?);
    }

    public async ValueTask<User> GetSystemUserAsync(bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return await Get(user => user.Role == RoleType.System, asNoTracking).FirstAsync(cancellationToken);
    }

    public ValueTask<User> UpdateAsync(User user, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
