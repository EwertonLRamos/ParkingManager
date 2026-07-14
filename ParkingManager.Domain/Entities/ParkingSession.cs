using System.ComponentModel.DataAnnotations;
using ParkingManager.Domain.Enums;

namespace ParkingManager.Domain.Entities;

public class ParkingSession
{
    public Guid Id { get; private set; }
    public string LicensePlate { get; private set; }
    public DateTime EntryTime { get; private set; }
    public DateTime? ExitTime { get; private set; }
    public double OccupancyRate { get; private set; }
    public SessionStatus Status { get; private set; }
    public decimal? SpotLat { get; private set; }
    public decimal? SpotLng { get; private set; }
    public decimal? TotalAmount { get; private set; }
    
    public ParkingSession(string licensePlate, DateTime entryTime, double occupancyRate)
    {
        Id = Guid.NewGuid();
        LicensePlate = licensePlate;
        EntryTime = entryTime;
        OccupancyRate = occupancyRate;
        Status = SessionStatus.Active;
    }

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