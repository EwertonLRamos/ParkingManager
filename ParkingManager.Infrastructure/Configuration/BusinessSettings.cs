using System.ComponentModel.DataAnnotations;
using ParkingManager.Domain.Abstractions;

namespace ParkingManager.Infrastructure.Configuration;

public class BusinessSettings : IBusinessSettings
{
    [Required]
    [MinLength(3)]
    [MaxLength(3)]
    public string Currency { get; set; } = default!;
}