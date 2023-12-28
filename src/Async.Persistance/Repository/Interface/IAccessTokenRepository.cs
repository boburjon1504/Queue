using Async.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Persistance.Repository.Interface;
public interface IAccessTokenRepository
{
    ValueTask<AccessToken> CreateAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default);

    ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default);

    ValueTask<AccessToken> UpdateAsync(AccessToken accessToken, CancellationToken cancellationToken = default);
}
