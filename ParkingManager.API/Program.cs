using Microsoft.EntityFrameworkCore;
using ParkingManager.API.Extensions;
using ParkingManager.Application.Commands.Entry;
using ParkingManager.Application.DependencyInjection.Extensions;
using ParkingManager.Domain.Repositories;
using ParkingManager.Infrastructure.Data;
using ParkingManager.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCommandHandlers(typeof(EntryCommandHandler).Assembly);

builder.Services.AddDbContext<ParkingManagerDbContext>(options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IParkingSessionRepository, ParkingSessionRepository>();
builder.Services.AddScoped<ISectorRepository, SectorRepository>();
builder.Services.AddScoped<ISpotRepository, SpotRepository>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

// Configure the HTTP request pipeline.
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