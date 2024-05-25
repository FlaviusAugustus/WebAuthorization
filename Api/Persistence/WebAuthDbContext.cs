using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Shared.Models;
using WebAppAuthorization.Constants;

namespace WebAppAuthorization.Persistence;

public class WebAuthDbContext(DbContextOptions<WebAuthDbContext> options, IConfiguration config)
    : IdentityDbContext<User, IdentityRole<Guid>, Guid>(options)
{
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite(config.GetConnectionString("Database"));
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        foreach (var role in Enum.GetNames<Role>())
        {
            modelBuilder.Entity<IdentityRole<Guid>>().HasData(
                new IdentityRole<Guid>
                {
                    Id = Guid.NewGuid(),
                    ConcurrencyStamp = Guid.NewGuid().ToString(),
                    Name = role,
                    NormalizedName = role.ToUpper()
                });
        }
        
        base.OnModelCreating(modelBuilder);
    } 
}