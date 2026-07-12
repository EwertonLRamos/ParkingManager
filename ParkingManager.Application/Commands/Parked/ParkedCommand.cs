using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.Commands.Parked;

public record ParkedCommand(string LicensePlate, string Latitude, string Longitude, EventType EventType);