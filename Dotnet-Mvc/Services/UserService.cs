using Dapper;
using Dotnet_Mvc.Dtos;
using Dotnet_Mvc.Entities;
using Dotnet_Mvc.Enums;
using Dotnet_Mvc.Providers;
using Dotnet_Mvc.Repository.Interface;
using Dotnet_Mvc.Services.Interface;

namespace Dotnet_Mvc.Services;

public class UserService : IUserService
{
    private readonly IUserRepo _userRepo;

    public UserService(IUserRepo userRepo)
    {
        _userRepo = userRepo;
    }

    public void AddUser(NewUserDto dto)
    {
        var model = new User
        {
            UserName = dto.UserName,
            Email = dto.Email ?? string.Empty,
            Address = dto.Address,
            Password = dto.Password,
        };
        _userRepo.Create(model);
    }

    public void EditUser(User user, UserEditDto dto)
    {
        user.UserName = dto.UserName;
        user.Email = dto.Email;
        user.Address = dto.Address;
        _userRepo.Update(user);
        _userRepo.Commit();
    }

    public void RemoveUserAsync(User user)
    {
        user.Status = (int)StatusEnum.Inactive;
        _userRepo.Update(user);
        _userRepo.Commit();
    }

    public async Task CreateUserAsync(NewUserDto dto)
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