using Dotnet_Mvc.Dtos;
using Dotnet_Mvc.Entities;
using Dotnet_Mvc.Models;
using Dotnet_Mvc.ViewModel;

namespace Dotnet_Mvc.Services.Interface;

public interface IUserService
{
    public Task<UserModel> AddUserAsync(NewUseDto dto);
    void EditUserAsync(EditUserVm vm);
    void RemoveUserAsync(Guid id);
    public Task CreateUserAsync(NewUseDto dto);
}