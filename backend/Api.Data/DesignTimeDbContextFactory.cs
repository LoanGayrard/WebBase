using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data;

public sealed class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // 1) Priority to env variables (CI/Docker)
        var cs = Environment.GetEnvironmentVariable("ConnectionStrings__Default");

        // 2) Fallback local dev (for dotnet ef)
        if (string.IsNullOrWhiteSpace(cs))
        {
            cs = "Host=localhost;Port=5432;Database=app;Username=postgres;Password=root";
        }

        var options = new DbContextOptionsBuilder<AppDbContext>()
            .UseNpgsql(cs, x => x.MigrationsAssembly("Api.Data"))
            .Options;

        return new AppDbContext(options);
    }
}
