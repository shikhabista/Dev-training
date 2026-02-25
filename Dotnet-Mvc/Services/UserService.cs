using Dapper;
using Dotnet_Mvc.Dtos;
using Dotnet_Mvc.Entities;
using Dotnet_Mvc.Enums;
using Dotnet_Mvc.Models;
using Dotnet_Mvc.Providers;
using Dotnet_Mvc.Services.Interface;
using Dotnet_Mvc.ViewModel;

namespace Dotnet_Mvc.Services;

public class UserService : IUserService
{
    private static List<UserModel> _list = new List<UserModel>();

    public async Task<UserModel> AddUserAsync(NewUseDto dto)
    {
        var model = new UserModel
        {
            Id = Guid.NewGuid(),
            UserName = dto.UserName,
            Email = dto.Email,
            Address = dto.Address,
            Password = dto.Password,
        };
        _list.Add(model);
        return model;
    }

    public void EditUserAsync(EditUserVm vm)
    {
        var details = _list.FirstOrDefault(x => x.Id == vm.UserId);
        if (details == null)
        {
            throw new Exception("User not found");
        }
        else
        {
            details.UserName = vm.UserName;
            details.Email = vm.Email;
            details.Address = vm.Address;
        }
    }

    public void RemoveUserAsync(Guid id)
    {
        var userData = _list.FirstOrDefault(x => x.Id == id);
        if (userData != null)
        {
            userData.Status = (int)StatusEnum.Inactive;
        }
        else
        {
            throw new Exception("User not found");
        }
    }

    public async Task CreateUserAsync(NewUseDto dto)
    {
        var conn = ConnectionProvider.GetConnection();
        var existing = "select * from public.users where user_name = @userName";
        var validation = conn.QueryFirstOrDefault(existing, new { userName = dto.UserName });

        if (validation != null)
        {
            throw new Exception("User already exists");
        }
        else
        {
            const string query =
                "INSERT INTO public.users ( user_name, email, password,address, rec_date, status)" +
                " values (@userName, @email, @password,@address, @recDate, @status)returning id";

            var newUserId = await conn.ExecuteAsync(query,
                new
                {
                    userName = dto.UserName, email = dto.Email, password = dto.Password, address = dto.Address,
                    recDate = DateTime.Now.ToUniversalTime(),
                    status = (int)StatusEnum.Active
                });
            conn.Close();
        }
    }
}