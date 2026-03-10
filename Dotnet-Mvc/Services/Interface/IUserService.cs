using Dotnet_Mvc.Dtos;
using Dotnet_Mvc.Entities;

namespace Dotnet_Mvc.Services.Interface;

public interface IUserService
{
    void AddUser(NewUserDto dto);
    void EditUser(User user, UserEditDto dto);
    void RemoveUserAsync(User user);
    Task CreateUserAsync(NewUserDto dto);
    User GetUser(string username, string password);
}