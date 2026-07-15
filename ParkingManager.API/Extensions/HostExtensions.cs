using ParkingManager.API.DTOs.Garage;
using ParkingManager.Domain.Entities;
using ParkingManager.Domain.Repositories;

namespace ParkingManager.API.Extensions;

public static class HostExtensions
{
    public static async Task<IHost> FillDatabaseAsync(this IHost host)
    {
        using var scope = host.Services.CreateScope();
        var services = scope.ServiceProvider;

        var sectorRepository = services.GetRequiredService<ISectorRepository>();
        var spotRepository = services.GetRequiredService<ISpotRepository>();
        var httpClientFactory = services.GetRequiredService<IHttpClientFactory>();

        var existingSectors = await sectorRepository.GetAllAsync();
        if (existingSectors.Count != 0)
            return host;

        try
        {
            var client = httpClientFactory.CreateClient();
            
            var response = await client.GetFromJsonAsync<GarageResponseDTO>("http://localhost:5000/garage");

            if (response != null)
            {
                var sectors = response.Sectors
                    .Select(s => new Sector(s.Name, s.BasePrice, s.MaxCapacity))
                    .ToList();

                await sectorRepository.AddRangeByBatchAsync(sectors);

                var sectorsIds = (await sectorRepository.GetAllAsync())
                    .ToDictionary(s => s.Name, s => s.Id);

                var spots = response.Spots
                    .Select(s => new Spot(s.Id, s.Latitude, s.Longitude, sectorsIds[s.SectorName]))
                    .ToList();

                await spotRepository.AddRangeByBatchAsync(spots);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Erro ao sincronizar garagem: {ex.Message}");
        }

        return host;
    }
}