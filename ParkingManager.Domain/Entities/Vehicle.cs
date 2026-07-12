namespace ParkingManager.Domain.Entities;

public class Vehicle
{
    public string LicensePlate { get; private set; }
    public string Model { get; private set; }
    public string Color { get; private set; }

    public Vehicle(string licensePlate, string model, string color)
    {
        LicensePlate = licensePlate;
        Model = model;
        Color = color;
    }
}