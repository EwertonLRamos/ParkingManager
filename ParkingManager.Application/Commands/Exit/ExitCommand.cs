using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.Commands.Exit;

public record ExitCommand(string LicensePlate, DateTime ExitTime, EventType EventType);