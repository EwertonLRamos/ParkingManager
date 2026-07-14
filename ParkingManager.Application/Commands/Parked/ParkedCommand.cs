using ParkingManager.Application.DependencyInjection.Interfaces;
using ParkingManager.Domain.Enums;

namespace ParkingManager.Application.Commands.Parked;

public record ParkedCommand(string LicensePlate, decimal Latitude, decimal Longitude, EventType EventType) : ICommand;