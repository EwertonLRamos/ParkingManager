using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.Commands.Parked;

public record ParkedCommand(string LicensePlate, DateTime ParkedTime, EventType EventType);