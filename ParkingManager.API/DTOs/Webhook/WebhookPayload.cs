using System.Text.Json.Serialization;
namespace ParkingManager.API.DTOs.Webhook;

public class WebhookPayload
{
    [JsonPropertyName("event_type")]
    public string EventType { get; set; }

    [JsonPropertyName("lat")]
    public decimal? Latitude { get; set; }

    [JsonPropertyName("lng")]
    public decimal? Longitude { get; set; }

    [JsonPropertyName("license_plate")]
    public string LicensePlate { get; set; }

    [JsonPropertyName("entry_time")]
    public DateTime? EntryTime { get; set; }

    [JsonPropertyName("exit_time")]
    public DateTime? ExitTime { get; set; }
}