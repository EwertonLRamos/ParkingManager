using ParkingManager.Domain.Enums;

namespace ParkingManager.API.Events;

public class WebhookEvent
{
    public EventType EventType { get; init; }
    public decimal? Latitude { get; init; }
    public decimal? Longitude { get; init; }
    public string LicensePlate { get; init; }
    public DateTime? Timestamp { get; init; }
}