using Async.Application.Common.Identity.Service;
using Async.Domain.Entities;
using Async.Persistance.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Infrostructure.Common.Identity.Service;
public class AccessTokenService(IAccessTokenRepository accessTokenRepository) : IAccessTokenService
{
    public ValueTask<AccessToken> CreateAsync(AccessToken accessToken, bool saveChanges = true, CancellationToken cancellationToken = default)
    {
        return accessTokenRepository.CreateAsync(accessToken, saveChanges, cancellationToken);
    }

    public ValueTask<AccessToken?> GetByIdAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        return accessTokenRepository.GetByIdAsync(accessTokenId, cancellationToken);
    }

    public async ValueTask RevokeAsync(Guid accessTokenId, CancellationToken cancellationToken = default)
    {
        var accessToken = await GetByIdAsync(accessTokenId, cancellationToken);
        if (accessToken is null) throw new InvalidOperationException($"Access token with id {accessTokenId} not found.");

        accessToken.IsRevoked = true;
        await accessTokenRepository.UpdateAsync(accessToken, cancellationToken);
    }
}
