using ParkingManager.Domain.Enums;

namespace ParkingManager.Domain.Entities;

public class ParkingSession
{
    public string LicensePlate { get; private set; }
    public DateTime EntryTime { get; private set; }
    public DateTime? ExitTime { get; private set; }
    public SessionStatus Status { get; private set; }
    public decimal? TotalAmount { get; private set; }
    
    public ParkingSession(string licensePlate, DateTime entryTime)
    {
        LicensePlate = licensePlate;
        EntryTime = entryTime;
        Status = SessionStatus.Active;
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