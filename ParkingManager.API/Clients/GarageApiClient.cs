using ParkingManager.API.DTOs.Garage;

namespace ParkingManager.API.Clients;

public class GarageApiClient(HttpClient httpClient)
{
    public async Task<GarageResponseDTO?> GetGarageAsync()
        => await httpClient.GetFromJsonAsync<GarageResponseDTO>("/garage");
}