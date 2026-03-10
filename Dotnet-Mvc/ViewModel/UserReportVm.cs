namespace Dotnet_Mvc.ViewModel;

public class UserReportVm
{
    public long Id { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public string? Address { get; set; }
    public string Password { get; set; }
    public string Status { get; set; }
    public string Name { get; set; }
    public string? ContactNo { get; set; }
    public bool IsActive { get; set; }
}