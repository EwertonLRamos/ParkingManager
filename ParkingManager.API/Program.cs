using ParkingManager.Application.Commands.Entry;
using ParkingManager.Application.Commands.Exit;
using ParkingManager.Application.Commands.Parked;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<EntryCommandHandler>();
builder.Services.AddScoped<ParkedCommandHandler>();
builder.Services.AddScoped<ExitCommandHandler>();

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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