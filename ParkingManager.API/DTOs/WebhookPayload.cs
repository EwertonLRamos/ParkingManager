using ParkingManager.Domain.Enums;

namespace ParkingManager.API.DTOs;

public class WebhookPayload
{
    public EventType EventType { get; set; }
    public string? Latitude { get; set; }
    public string? Longitude { get; set; }
    public string LicensePlate { get; set; }
    public DateTime? Timestamp { get; set; }
}