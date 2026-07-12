namespace ParkingManager.Domain.Entities;

public class ParkingSession
{
    public string LicensePlate { get; private set; }
    public DateTime EntryTime { get; private set; }
    
    public ParkingSession(string licensePlate, DateTime entryTime)
    {
        LicensePlate = licensePlate;
        EntryTime = entryTime;
    }
}