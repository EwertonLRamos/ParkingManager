using System.Text.Json.Serialization;

namespace ParkingManager.API.DTOs.Garage;

public class SectorDTO
{
    [JsonPropertyName("sector")]
    public string Name { get; set; }

    [JsonPropertyName("basePrice")]
    public decimal BasePrice { get; set; }
    
    [JsonPropertyName("max_capacity")]
    public int MaxCapacity { get; set; }
}