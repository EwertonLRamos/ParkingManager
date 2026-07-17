using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using ParkingManager.API.Clients;
using ParkingManager.API.Clients.Settings;
using ParkingManager.API.Extensions;
using ParkingManager.Application.Commands.Entry;
using ParkingManager.Application.DependencyInjection.Extensions;
using ParkingManager.Domain.Abstractions;
using ParkingManager.Domain.Repositories;
using ParkingManager.Infrastructure.Configuration;
using ParkingManager.Infrastructure.Data;
using ParkingManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCommandHandlers(typeof(EntryCommandHandler).Assembly);

builder.Services.AddDbContext<ParkingManagerDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddOptions<BusinessSettings>()
    .Bind(builder.Configuration.GetSection("BusinessSettings"))
    .ValidateDataAnnotations()
    .ValidateOnStart();

builder.Services.AddSingleton<IBusinessSettings>(sp =>
    sp.GetRequiredService<IOptions<BusinessSettings>>().Value);

builder.Services.AddScoped<IParkingSessionRepository, ParkingSessionRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<ISpotRepository, SpotRepository>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();
builder.Services.AddHttpClient<GarageApiClient>((sp, client) =>
{
    var settings = sp
        .GetRequiredService<IOptions<GarageApiSettings>>()
        .Value;

    client.BaseAddress = new Uri(settings.BaseUrl);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.MapControllers();

await app.FillDatabaseAsync();

app.Run();