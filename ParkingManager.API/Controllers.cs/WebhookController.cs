using Microsoft.AspNetCore.Mvc;
using ParkingManager.API.DTOs;
using ParkingManager.Application.Commands.Entry;
using ParkingManager.Application.Commands.Exit;
using ParkingManager.Application.Commands.Parked;
using ParkingManager.Domain.Enums;

namespace ParkingManager.API.Controllers;

[ApiController]
[Route("webhook")]
public class WebhookController : ControllerBase
{
    private readonly EntryCommandHandler _entryCommandHandler;
    private readonly ParkedCommandHandler _parkedCommandHandler;
    private readonly ExitCommandHandler _exitCommandHandler;

    public WebhookController(EntryCommandHandler entryCommandHandler, ParkedCommandHandler parkedCommandHandler, ExitCommandHandler exitCommandHandler)
    {
        _entryCommandHandler = entryCommandHandler;
        _parkedCommandHandler = parkedCommandHandler;
        _exitCommandHandler = exitCommandHandler;
    }

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

                _entryCommandHandler.Handle(entryCommand);
                break;

            case EventType.Parked:
                var parkedCommand = new ParkedCommand(
                    payload.LicensePlate, 
                    payload.Latitude, 
                    payload.Longitude, 
                    payload.EventType){};

                _parkedCommandHandler.Handle(parkedCommand);
                break;

            case EventType.Exit:
                var exitCommand = new ExitCommand(
                    payload.LicensePlate, 
                    payload.Timestamp ?? DateTime.UtcNow, 
                    payload.EventType){};

                _exitCommandHandler.Handle(exitCommand);
                break;
        }

        return Ok();
    }
}