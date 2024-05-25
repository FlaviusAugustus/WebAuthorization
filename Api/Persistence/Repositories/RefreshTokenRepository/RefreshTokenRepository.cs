using Microsoft.EntityFrameworkCore;
using Shared.Models;

namespace WebAppAuthorization.Persistence.Repositories.RefreshTokenRepository;

public class RefreshTokenRepository(WebAuthDbContext context) : 
    GenericRepository<RefreshToken>(context), IRefreshTokenRepository
{
    public async Task<RefreshToken?> GetByTokenHash(string tokenHash) =>
        await context.Set<RefreshToken>()
            .Where(t => t.TokenHash == tokenHash)
            .SingleOrDefaultAsync();
    
}