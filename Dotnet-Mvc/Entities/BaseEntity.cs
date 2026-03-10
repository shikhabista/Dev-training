using System.ComponentModel.DataAnnotations;
using Dotnet_Mvc.Enums;

namespace Dotnet_Mvc.Entities;

public class BaseEntity
{
    [Key] public int Id { get; set; }
    public DateTime RecDate { get; set; } = DateTime.Now.ToUniversalTime();
    public StatusEnum Status { get; set; } = StatusEnum.Active;
    public RecStatusEnum RecStatus { get; set; } = RecStatusEnum.Active;
}