using ParkingManager.Domain.Enums;

namespace ParkingManager.Domain.Entities;

public class ParkingSession(string licensePlate, DateTime entryTime, double occupancyRate)
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string LicensePlate { get; private set; } = licensePlate;
    public DateTime EntryTime { get; private set; } = entryTime;
    public DateTime? ExitTime { get; private set; }
    public double OccupancyRate { get; private set; } = occupancyRate;
    public SessionStatus Status { get; private set; } = SessionStatus.Active;
    public decimal? TotalAmount { get; private set; }

    public int? SpotId { get; private set; }
    public Spot? Spot { get; private set; }

    public Guid? SectorId { get; private set; }
    public Sector? Sector { get; private set; }

    public void Park(Spot spot)
    {
        if (Status != SessionStatus.Active)
            throw new InvalidOperationException("Session is not active.");

        SpotId = spot.Id;
        Spot = spot;
        SectorId = spot.SectorId;
        Sector = spot.Sector;
    }

    public void Finish(DateTime exitTime, decimal totalAmount)
    {
        if (Status == SessionStatus.Finished)
        {
            throw new InvalidOperationException("Session is already finished.");
        }

        ExitTime = exitTime;
        TotalAmount = totalAmount;
        Status = SessionStatus.Finished;
    }
}