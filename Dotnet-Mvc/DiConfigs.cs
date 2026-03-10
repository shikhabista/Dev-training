using Dotnet_Mvc.Data;
using Dotnet_Mvc.Repository;
using Dotnet_Mvc.Repository.Interface;
using Dotnet_Mvc.Services;
using Dotnet_Mvc.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Mvc;

public static class DiConfigs
{
    public static void ConfigureServices(this IServiceCollection service)
    {
        service.AddScoped<DbContext, ApplicationDbContext>();
        service.AddScoped<IUserService, UserService>();
        service.AddScoped<IUserRepo, UserRepo>();
    }
}