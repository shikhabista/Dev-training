using System.ComponentModel.DataAnnotations;
using Dotnet_Mvc.Enums;

namespace Dotnet_Mvc.Entities;

public class BaseEntity
{
    [Key] public int Id { get; set; }
    public DateTime RecDate { get; set; } = DateTime.Now.ToUniversalTime();
    public int Status { get; set; } = (int)StatusEnum.Active;
    public char RecStatus { get; set; } = (char)RecStatusEnum.Active;
}