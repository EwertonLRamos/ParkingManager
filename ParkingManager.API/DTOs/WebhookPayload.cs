using ParkingManager.Domain.Enums;

namespace ParkingManager.API.DTOs;

public class WebhookPayload
{
    public EventType EventType { get; set; }
    public decimal? Latitude { get; set; }
    public decimal? Longitude { get; set; }
    public string LicensePlate { get; set; }
    public DateTime? Timestamp { get; set; }
}