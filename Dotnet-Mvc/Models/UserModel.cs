using Dotnet_Mvc.Enums;

namespace Dotnet_Mvc.Models;

public class UserModel
{
    public Guid Id { get; set; }
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string Password { get; set; }
    public int Status { get; set; } = (int)StatusEnum.Active;
}