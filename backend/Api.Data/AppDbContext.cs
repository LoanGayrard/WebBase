using Api.Data.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Api.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<AppUser>(options)
{
    public DbSet<Todo> Todos => Set<Todo>();

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        // Default schema
        builder.HasDefaultSchema("app");

        // Identity schem
        builder.Entity<AppUser>().ToTable("AspNetUsers", "Identity");
        builder.Entity<IdentityRole>().ToTable("AspNetRoles", "Identity");
        builder.Entity<IdentityUserRole<string>>().ToTable("AspNetUserRoles", "Identity");
        builder.Entity<IdentityUserClaim<string>>().ToTable("AspNetUserClaims", "Identity");
        builder.Entity<IdentityUserLogin<string>>().ToTable("AspNetUserLogins", "Identity");
        builder.Entity<IdentityRoleClaim<string>>().ToTable("AspNetRoleClaims", "Identity");
        builder.Entity<IdentityUserToken<string>>().ToTable("AspNetUserTokens", "Identity");
    }
}
