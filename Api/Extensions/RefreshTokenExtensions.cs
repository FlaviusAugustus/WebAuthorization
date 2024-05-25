using System.Security.Cryptography;
using System.Text;
using Shared.Models;

namespace WebAppAuthorization.Extensions;

public static class RefreshTokenHelpers
{
    public static RefreshToken NewTokenValue(string refreshToken, string accessToken, DateTime exp)
    {
        return new RefreshToken
        {
            CreatedAt = DateTime.Now,
            Id = Guid.NewGuid(),
            AccessTokenHash = Hashed(accessToken),
            TokenHash = Hashed(refreshToken),
            ExpirationDate = exp,
            Revoked = false
        };
    }

    public static string Hashed(string token)
    {
        var rawHash = SHA256.HashData(Encoding.UTF8.GetBytes(token));
        
        return Encoding.UTF8.GetString(rawHash);
    }
}