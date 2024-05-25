using Shared.Models;

namespace WebAppAuthorization.Persistence.Repositories.RefreshTokenRepository;

public interface IRefreshTokenRepository : IGenericRepository<RefreshToken>
{
    public Task<RefreshToken?> GetByTokenHash(string tokenHash);
}