using Dotnet_Mvc.Entities;

namespace Dotnet_Mvc.Repository.Interface;

public interface IUserRepo
{
    void Create(User user);
    void Update(User user);
    void Remove(User user);
    IQueryable<User> GetQueryable();
    void Commit();
}