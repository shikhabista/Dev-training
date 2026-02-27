using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_Mvc.Entities
{
    [Table("user", Schema = "admin")]
    public class User : BaseEntity
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public string? Address { get; set; }
    }
}