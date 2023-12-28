using Async.Domain.Entities;
using Async.Persistance.Cashing.Broker;
using Async.Persistance.DataContext;
using Async.Persistance.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.Repository;
public class UserSettingsRepository(NotificationDbContext dbContext, ICacheBroker cacheBroker)
    : EntityRepositoryBase<UserSettings, NotificationDbContext>(dbContext, cacheBroker), IUserSettingsRepository
{
    public new ValueTask<UserSettings> CreateAsync(UserSettings userSettings, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return base.CreateAsync(userSettings, saveChanges, cancellationToken);
    }

    public new ValueTask<UserSettings?> GetByIdAsync(Guid userId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return base.GetByIdAsync(userId, asNoTracking, cancellationToken);
    }
}
