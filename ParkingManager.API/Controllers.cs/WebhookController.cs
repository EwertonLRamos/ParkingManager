using Microsoft.AspNetCore.Mvc;
using ParkingManager.API.DTOs.Webhook;
using ParkingManager.Application.Commands.Entry;
using ParkingManager.Application.Commands.Exit;
using ParkingManager.Application.Commands.Parked;
using ParkingManager.Application.DependencyInjection.Dispatchers;
using ParkingManager.Domain.Enums;

namespace ParkingManager.API.Controllers;

[ApiController]
[Route("webhook")]
public class WebhookController(CommandDispatcher commandDispatcher) : ControllerBase
{
    private readonly CommandDispatcher _commandDispatcher = commandDispatcher;

    [HttpPost]
    public async Task<IActionResult> ReceiveEvent([FromBody] WebhookPayload payload)
    {
        switch (payload.EventType)
        {
            case EventType.Entry:
                var entryCommand = new EntryCommand(
                    payload.LicensePlate, 
                    payload.Timestamp ?? DateTime.UtcNow, 
                    payload.EventType){};

                await _commandDispatcher.DispatchAsync(entryCommand);
                break;

            case EventType.Parked:
                var parkedCommand = new ParkedCommand(
                    payload.LicensePlate, 
                    payload.Latitude!.Value, 
                    payload.Longitude!.Value, 
                    payload.EventType){};

                await _commandDispatcher.DispatchAsync(parkedCommand);
                break;

            case EventType.Exit:
                var exitCommand = new ExitCommand(
                    payload.LicensePlate, 
                    payload.Timestamp ?? DateTime.UtcNow, 
                    payload.EventType){};

                await _commandDispatcher.DispatchAsync(exitCommand);
                break;
        }

        return Ok();
    }
}