using BusinessObjects.Context;
using Microsoft.EntityFrameworkCore;

namespace ChemistryProjectPrep.API.Configurations;

public static class DatabaseConfiguration
{
    public static IServiceCollection ConfigureDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new ArgumentException("Connection string 'DefaultConnection' is not configured.");
        }
        services.AddDbContext<ChemProjectDbContext>(options =>
            options.UseNpgsql(connectionString)); // Changed from UseSqlServer to UseNpgsql
        return services;
    }
}