using System.Text.Json.Serialization;

namespace ParkingManager.API.DTOs.Garage;

public class GarageResponseDTO
{
    [JsonPropertyName("garage")]
    public List<SectorDTO> Sectors { get; set; } = [];
    
    [JsonPropertyName("spots")]
    public List<SpotDTO> Spots { get; set; } = [];
}