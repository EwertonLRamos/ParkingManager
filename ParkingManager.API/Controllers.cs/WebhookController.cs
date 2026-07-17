using Microsoft.AspNetCore.Mvc;
using ParkingManager.API.DTOs.Webhook;
using ParkingManager.API.Factories;
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
        var webhookEvent = WebhookEventFactory.Create(payload);
        
        switch (webhookEvent.EventType)
        {
            case EventType.Entry:
                var entryCommand = new EntryCommand(
                    webhookEvent.LicensePlate, 
                    webhookEvent.Timestamp ?? DateTime.UtcNow, 
                    webhookEvent.EventType){};

                await _commandDispatcher.DispatchAsync(entryCommand);
                break;

            case EventType.Parked:
                var parkedCommand = new ParkedCommand(
                    webhookEvent.LicensePlate, 
                    webhookEvent.Latitude!.Value, 
                    webhookEvent.Longitude!.Value, 
                    webhookEvent.EventType){};

                await _commandDispatcher.DispatchAsync(parkedCommand);
                break;

            case EventType.Exit:
                var exitCommand = new ExitCommand(
                    webhookEvent.LicensePlate, 
                    webhookEvent.Timestamp ?? DateTime.UtcNow, 
                    webhookEvent.EventType){};

                await _commandDispatcher.DispatchAsync(exitCommand);
                break;
        }

        return Ok();
    }
}