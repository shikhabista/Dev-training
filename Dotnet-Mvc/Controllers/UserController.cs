using Dotnet_Mvc.Dtos;
using Dotnet_Mvc.Models;
using Dotnet_Mvc.Services.Interface;
using Dotnet_Mvc.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace Dotnet_Mvc.Controllers;

public class UserController : Controller
{
    private static List<UserModel> _list = new();
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddUserVm vm)
    {
        try
        {
            var existing = _list.FirstOrDefault(x => x.UserName == vm.Name.Trim());
            if (existing == null)
            {
                var dto = new NewUseDto
                {
                    UserName = vm.Name,
                    Email = vm.Email,
                    Address = vm.Address,
                    Password = vm.Pass
                };
                var data = await _userService.AddUserAsync(dto);
                _list.Add(data);
                return RedirectToAction("UserReport");
            }
            else
            {
                throw new Exception("User with same name already exists");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IActionResult UserReport()
    {
        var report = _list;
        return View(report);
    }

    public IActionResult Edit(Guid id)
    {
        try
        {
            var existing = _list.FirstOrDefault(x => x.Id == id);
            if (existing == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                var res = new EditUserVm
                {
                    UserId = existing.Id,
                    UserName = existing.UserName,
                    Email = existing.Email,
                    Address = existing.Address,
                };
                return View(res);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserVm vm)
    {
        try
        {
            var details = _list.Find(x => x.Id == vm.UserId);

            if (details == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                _userService.EditUserAsync(vm);
            }

            return RedirectToAction("UserReport");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var user = _list.FirstOrDefault(x => x.Id == id);
            if (user == null)
            {
                throw new Exception("User not found");
            }
            else
            {
                _userService.RemoveUserAsync(user.Id);
                return RedirectToAction("UserReport");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }


    public async Task<IActionResult> Add(AddUserVm vm)
    {
        try
        {
            var dto = new NewUseDto
            {
                UserName = vm.Name.Trim(),
                Email = vm.Email,
                Address = vm.Address,
                Password = vm.Pass
            };
            await _userService.CreateUserAsync(dto);
            return RedirectToAction("UserReport");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}