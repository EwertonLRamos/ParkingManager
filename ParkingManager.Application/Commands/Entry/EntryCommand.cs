using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.Commands.Entry;

public record EntryCommand(string LicensePlate, DateTime EntryTime, EventType EventType);