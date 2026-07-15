using System.Text.Json.Serialization;

namespace ParkingManager.API.DTOs.Garage;

public class SpotDTO
{
    [JsonPropertyName("id")]
    public int Id { get; set; }
    
    [JsonPropertyName("sector")]
    public string SectorName { get; set; }
    
    [JsonPropertyName("lat")]
    public decimal Latitude { get; set; }
    
    [JsonPropertyName("lng")]
    public decimal Longitude { get; set; }
}