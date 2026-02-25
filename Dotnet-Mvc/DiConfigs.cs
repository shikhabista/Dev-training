using Dotnet_Mvc.Providers;
using Dotnet_Mvc.Services;
using Dotnet_Mvc.Services.Interface;

namespace Dotnet_Mvc;

public static class DiConfigs
{
    public static void UseDbContext(this WebApplicationBuilder builder)
    {
        ConnectionProvider.Initialize(builder.Configuration);
        builder.UseApplicationServices();
    }

    public static void UseApplicationServices(this WebApplicationBuilder builder)
    {
        builder.Services.AddScoped<IUserService, UserService>();
    }
}