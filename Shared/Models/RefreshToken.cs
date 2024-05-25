namespace Shared.Models;

public class RefreshToken : Entity
{
    public bool Revoked { get; set; } 
    public string TokenHash { get; set; } = string.Empty;
    public string AccessTokenHash { get; set; } = string.Empty;
    public DateTime ExpirationDate { get; set; } = DateTime.UnixEpoch;
}