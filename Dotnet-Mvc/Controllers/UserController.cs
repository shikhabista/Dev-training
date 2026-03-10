using Dotnet_Mvc.Dtos;
using Dotnet_Mvc.Models;
using Dotnet_Mvc.Repository.Interface;
using Dotnet_Mvc.Services.Interface;
using Dotnet_Mvc.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Mvc.Controllers;

public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserRepo _userRepo;

    public UserController(IUserService userService, IUserRepo userRepo)
    {
        _userService = userService;
        _userRepo = userRepo;
    }

    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(AddUserVm vm)
    {
        try
        {
            var dto = new NewUserDto
            {
                UserName = vm.Name,
                Email = vm.Email,
                Address = vm.Address,
                Password = vm.Password
            };
            _userService.AddUser(dto);
            return RedirectToAction("UserReport");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public async Task<IActionResult> UserReport()
    {
        var report = await _userRepo.GetQueryable().ToListAsync();
        return View(report);
    }

    public async Task<IActionResult> Edit(long id)
    {
        try
        {
            var user = await _userRepo.GetQueryable().Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var res = new EditUserVm
            {
                UserId = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Address = user.Address,
            };
            return View(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(UserReport));
        }
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditUserVm vm)
    {
        try
        {
            var user = await _userRepo.GetQueryable().Where(a => a.Id == vm.UserId).FirstOrDefaultAsync();
            if (user == null)
            {
                throw new Exception("User not found");
            }

            var dto = new UserEditDto
            {
                UserName = vm.UserName,
                Email = vm.Email,
                Address = vm.Address
            };
            _userService.EditUser(user, dto);
            
            return RedirectToAction("UserReport");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(UserReport));
        }
    }

    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var user = await _userRepo.GetQueryable().Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
                throw new Exception("User not found");
            _userService.RemoveUserAsync(user);
            return RedirectToAction("UserReport");
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
            var dto = new NewUserDto
            {
                UserName = vm.Name.Trim(),
                Email = vm.Email,
                Address = vm.Address,
                Password = vm.Password
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