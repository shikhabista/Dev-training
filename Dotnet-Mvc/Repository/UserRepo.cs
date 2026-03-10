using Dotnet_Mvc.Entities;
using Dotnet_Mvc.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Mvc.Repository;

public class UserRepo(DbContext context) : IUserRepo
{
    public void Create(User user) => context.Set<User>().Add(user);
    public void Update(User user) => context.Set<User>().Update(user);
    public void Remove(User user) => context.Set<User>().Remove(user);
    public IQueryable<User> GetQueryable() => context.Set<User>();
    public void Commit() => context.SaveChanges();
}