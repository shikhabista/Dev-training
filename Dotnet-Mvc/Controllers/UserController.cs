using Dotnet_Mvc.Dtos;
using Dotnet_Mvc.Enums;
using Dotnet_Mvc.Models;
using Dotnet_Mvc.Repository.Interface;
using Dotnet_Mvc.Services.Interface;
using Dotnet_Mvc.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Dotnet_Mvc.Controllers;

[Authorize]
public class UserController : Controller
{
    private readonly IUserService _userService;
    private readonly IUserRepo _userRepo;

    public UserController(IUserService userService, IUserRepo userRepo)
    {
        _userService = userService;
        _userRepo = userRepo;
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpGet]
    public IActionResult Create()
    {
        return View();
    }

    [Authorize(Roles = "Admin, Manager")]
    [HttpPost]
    public async Task<IActionResult> Create(AddUserVm vm)
    {
        try
        {
            if (!vm.Password.Equals(vm.ConfirmPassword))
                throw new Exception("Passwords do not match");
            var dto = new NewUserDto
            {
                Name = vm.Name,
                ContactNo = vm.ContactNo,
                UserName = vm.Username,
                Email = vm.Email,
                Address = vm.Address,
                Password = vm.Password,
            };
            _userService.AddUser(dto);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Index));
        }
    }

    public async Task<IActionResult> Index()
    {
        var report = await _userRepo.GetQueryable().ToListAsync();
        var vm = report.Select(a => new UserReportVm()
        {
            Id = a.Id,
            Username = a.Username,
            Email = a.Email,
            Address = a.Address,
            Status = a.Status.ToString(),
            Name = a.Name,
            ContactNo = a.ContactNo,
            IsActive = a.Status == StatusEnum.Active
        }).ToList();
        return View(vm);
    }

    [Authorize(Roles = "Admin, Manager")]
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
                Name = user.Name,
                ContactNo = user.ContactNo,
                UserId = user.Id,
                UserName = user.Username,
                Email = user.Email,
                Address = user.Address,
            };
            return View(res);
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Index));
        }
    }
    
    [Authorize(Roles = "Admin, Manager")]
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
                Name = vm.Name,
                ContactNo = vm.ContactNo,
                UserName = vm.UserName,
                Email = vm.Email,
                Address = vm.Address
            };
            _userService.EditUser(user, dto);
            
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return RedirectToAction(nameof(Index));
        }
    }

    [Authorize(Roles = "Admin, Manager")]
    public async Task<IActionResult> Delete(long id)
    {
        try
        {
            var user = await _userRepo.GetQueryable().Where(a => a.Id == id).FirstOrDefaultAsync();
            if (user == null)
                throw new Exception("User not found");
            _userService.RemoveUserAsync(user);
            return RedirectToAction("Index");
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
                UserName = vm.Username.Trim(),
                Email = vm.Email,
                Address = vm.Address,
                Password = vm.Password
            };
            await _userService.CreateUserAsync(dto);
            return RedirectToAction("Index");
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public IActionResult AssignRole()
    {
        return View();
    }
}