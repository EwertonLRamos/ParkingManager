using Microsoft.AspNetCore.Mvc;
using ParkingManager.Application.DependencyInjection.Dispatchers;
using ParkingManager.Application.Queries.Revenue;

namespace ParkingManager.API.Controllers;

[ApiController]
[Route("revenue")]
public class RevenueController(CommandDispatcher commandDispatcher) : ControllerBase
{
    private readonly CommandDispatcher _commandDispatcher = commandDispatcher;


    [HttpGet]
    public async Task<IActionResult> GetRevenue([FromQuery] string sector, [FromQuery] DateOnly date)
    {
        var revenueQuery = new RevenueQuery(date, sector);

        var revenueResponse = await _commandDispatcher.DispatchAsync<RevenueQuery, RevenueQueryResponse>(revenueQuery);

        return Ok(revenueResponse);
    }
}