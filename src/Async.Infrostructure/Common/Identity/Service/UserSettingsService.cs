using Async.Application.Common.Identity.Service;
using Async.Domain.Entities;
using Async.Persistance.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Identity.Service;
public class UserSettingsService(IUserSettingsRepository userSettingsRepository) : IUserSettingsService
{
    public ValueTask<UserSettings> CreateAsync(UserSettings userSettings, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return userSettingsRepository.CreateAsync(userSettings, saveChanges, cancellationToken);
    }

    public ValueTask<UserSettings?> GetByIdAsync(Guid userSettingsId, bool asNoTracking = false, CancellationToken cancellationToken = default)
    {
        return userSettingsRepository.GetByIdAsync(userSettingsId, asNoTracking, cancellationToken);
    }
}
