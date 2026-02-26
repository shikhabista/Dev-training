using Dotnet_Mvc.Entities;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Mvc.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
}