namespace Dotnet_Mvc.Dtos;

public class NewUserDto
{
    public NewUserDto()
    {
    }
    
    public string Name { get; set; }
    public string? ContactNo { get; set; }
    public string UserName { get; set; }
    public string? Email { get; set; }
    public string? Address { get; set; }
    public string Password { get; set; }
}