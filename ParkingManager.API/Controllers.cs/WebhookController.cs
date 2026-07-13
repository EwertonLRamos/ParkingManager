using Microsoft.AspNetCore.Mvc;
using ParkingManager.API.DTOs;
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

                _commandDispatcher.Dispatch(entryCommand);
                break;

            case EventType.Parked:
                var parkedCommand = new ParkedCommand(
                    payload.LicensePlate, 
                    payload.Latitude, 
                    payload.Longitude, 
                    payload.EventType){};

                _commandDispatcher.Dispatch(parkedCommand);
                break;

            case EventType.Exit:
                var exitCommand = new ExitCommand(
                    payload.LicensePlate, 
                    payload.Timestamp ?? DateTime.UtcNow, 
                    payload.EventType){};

                _commandDispatcher.Dispatch(exitCommand);
                break;
        }

        return Ok();
    }
}