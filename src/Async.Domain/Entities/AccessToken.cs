using Async.Domain.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Async.Domain.Entities;
public  class AccessToken:Entity
{
    public AccessToken()
    {
    }

    public AccessToken(Guid id, Guid userId, string token, DateTimeOffset expiryTime, bool isRevoked)
    {
        Id = id;
        UserId = userId;
        Token = token;
        ExpiryTime = expiryTime;
        IsRevoked = isRevoked;
    }

    public Guid UserId { get; set; }

    public string Token { get; set; } = default!;

    public DateTimeOffset ExpiryTime { get; set; }

    public bool IsRevoked { get; set; }
}
