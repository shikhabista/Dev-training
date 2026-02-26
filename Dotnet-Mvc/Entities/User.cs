using System.ComponentModel.DataAnnotations.Schema;

namespace Dotnet_Mvc.Entities
{
    [Table("user", Schema = "admin")]
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
    }
}