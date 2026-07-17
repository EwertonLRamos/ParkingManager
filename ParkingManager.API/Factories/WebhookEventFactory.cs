using ParkingManager.API.DTOs.Webhook;
using ParkingManager.API.Events;
using ParkingManager.Domain.Enums;

namespace ParkingManager.API.Factories;

public static class WebhookEventFactory
{
    public static WebhookEvent Create(WebhookPayload payload)
    {
        var eventType = ParseEventType(payload.EventType);

        var timestamp = ResolveTimestamp(eventType, payload);

        return new WebhookEvent
        {
            EventType = eventType,
            Latitude = payload.Latitude,
            Longitude = payload.Longitude,
            LicensePlate = payload.LicensePlate,
            Timestamp = timestamp
        };
    }

    private static EventType ParseEventType(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            throw new ArgumentException("event_type is required");

        return value.ToUpperInvariant() switch
        {
            "ENTRY" => EventType.Entry,
            "PARKED" => EventType.Parked,
            "EXIT" => EventType.Exit,
            _ => throw new ArgumentException($"Invalid event_type: {value}")
        };
    }

    private static DateTime? ResolveTimestamp(EventType type, WebhookPayload payload)
    {
        return type switch
        {
            EventType.Entry => payload.EntryTime,
            EventType.Exit => payload.ExitTime,
            _ => null
        };
    }
}