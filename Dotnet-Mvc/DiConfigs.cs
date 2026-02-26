using Dotnet_Mvc.Data;
using Dotnet_Mvc.Providers;
using Dotnet_Mvc.Services;
using Dotnet_Mvc.Services.Interface;
using Microsoft.EntityFrameworkCore;


namespace Dotnet_Mvc;

public static class DiConfigs
{
    public static void UseApplication(this WebApplicationBuilder builder)
    {
        builder.Services.UseApplicationServices();
        builder.UseDbConnection();
    }

    static void UseDbConnection(this WebApplicationBuilder builder)
    {
        var connectionString = ConnectionProvider.Initialize(builder.Configuration);
        if (string.IsNullOrEmpty(connectionString))
        {
            throw new InvalidOperationException("No connection string found");
        }

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(connectionString));
    }

    static void UseApplicationServices(this IServiceCollection service)
    {
        service.AddScoped<IUserService, UserService>();
    }
}