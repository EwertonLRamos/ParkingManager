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
    public decimal? SpotLat { get; private set; }
    public decimal? SpotLng { get; private set; }
    public decimal? TotalAmount { get; private set; }

    public void Park(decimal? spotLat, decimal? spotLng)
    {
        if (Status != SessionStatus.Active)
            throw new InvalidOperationException("Session is not active.");

        SpotLat = spotLat;
        SpotLng = spotLng;
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