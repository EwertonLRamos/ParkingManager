namespace ParkingManager.Domain.Entities;

public class Spot(decimal latitude, decimal longitude, Guid sectorId)
{
    public int Id { get; private set; }
    public decimal Latitude { get; private set; } = latitude;
    public decimal Longitude { get; private set; } = longitude;
    public bool IsOccupied { get; private set; } = false;

    public Guid SectorId { get; private set; } = sectorId;
    public Sector Sector { get; private set; } = null!;

    public List<ParkingSession> ParkingSessions { get; private set; } = [];

    public void Occupy()
    {
        if (IsOccupied)
            throw new InvalidOperationException("Spot is already occupied.");

        IsOccupied = true;
    }

    public void Release()
    {
        if (!IsOccupied)
            throw new InvalidOperationException("Spot is already vacant.");

        IsOccupied = false;
    }
}