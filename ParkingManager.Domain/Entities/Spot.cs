using System.ComponentModel.DataAnnotations;

namespace ParkingManager.Domain.Entities;

public class Spot
{
    public int Id { get; private set; }
    public decimal Latitude { get; private set; }
    public decimal Longitude { get; private set; }
    public bool IsOccupied { get; private set; }

    public Guid SectorId { get; private set; }
    public Sector Sector { get; private set; }

    public Spot(decimal latitude, decimal longitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        IsOccupied = false;
    }
}