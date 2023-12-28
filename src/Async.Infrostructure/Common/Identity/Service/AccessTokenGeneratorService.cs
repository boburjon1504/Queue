using Async.Application.Common.Identity.Service;
using Async.Domain.Constants;
using Async.Domain.Entities;
using Async.Infrostructure.Common.Setting;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Async.Infrostructure.Common.Identity.Service;
public class AccessTokenGeneratorService(IOptions<JwtSettings> jwtSettings) : IAccessTokenGeneratorService
{
    private readonly JwtSettings _jwtSettings = jwtSettings.Value;

    public AccessToken GetToken(User user)
    {
        var accessToken = new Domain.Entities.AccessToken
        {
            Id = Guid.NewGuid()
        };
        var jwtToken = GetJwtToken(user, accessToken);
        var token = new JwtSecurityTokenHandler().WriteToken(jwtToken);
        accessToken.Token = token;

        return accessToken;
    }
    public JwtSecurityToken GetJwtToken(User user, AccessToken accessToken)
    {
        var security = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.SecretKey));
        var credential = new SigningCredentials(security,SecurityAlgorithms.HmacSha256);
        var claims = GetClaims(user, accessToken);
        return new JwtSecurityToken(
            issuer: _jwtSettings.ValidIssuer,
            audience: _jwtSettings.ValidAudience,
            expires: DateTime.Now.AddMinutes(_jwtSettings.ExpirationTimeInMinutes),
            signingCredentials: credential,
            claims: claims
            );
            
    }
    public List<Claim> GetClaims(User user, AccessToken accessToken)
    {
        return new List<Claim>
        {
            new Claim(ClaimTypes.Role,user.Role.ToString()),
            new Claim(ClaimConstants.UserId,user.Id.ToString()),
            new Claim(ClaimConstants.AccessTokenId,accessToken.Id.ToString())
        };
    }
    public Guid GetTokenId(string accessToken)
    {
        var tokenValue = accessToken.Split(' ')[1];
        var handler = new JwtSecurityTokenHandler();
        var token = handler.ReadJwtToken(tokenValue);
        var tokenId = token.Claims.FirstOrDefault(c => c.Type == ClaimConstants.AccessTokenId)?.Value;

        if (string.IsNullOrEmpty(tokenId))
            throw new ArgumentException("Invalid access token");

        return Guid.Parse(tokenId);
    }
}
