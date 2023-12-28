namespace Async.Api.Model.Dto;

public class AccessTokenDto
{
    public string Token { get; init; } = default!;

    public DateTimeOffset ExpiryTime { get; init; }
}
