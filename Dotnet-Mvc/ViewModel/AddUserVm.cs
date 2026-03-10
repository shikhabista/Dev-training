namespace Dotnet_Mvc.ViewModel;

public class AddUserVm
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string Password { get; set; }
}