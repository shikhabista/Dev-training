using Dotnet_Mvc.Dtos;
using Dotnet_Mvc.Entities;

namespace Dotnet_Mvc.Services.Interface;

public interface IUserService
{
    void AddUser(NewUserDto dto);
    void EditUser(User user, UserEditDto dto);
    void RemoveUserAsync(User user);
    public Task CreateUserAsync(NewUserDto dto);
}