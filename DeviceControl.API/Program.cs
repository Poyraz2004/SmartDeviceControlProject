using DeviceControl.DataAccess;
using DeviceControl.Entities;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Register services
builder.Services.AddSingleton<IDeviceRepository, DeviceManager>();

// Register Swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "DeviceControl API", Version = "v1" });
});

var app = builder.Build();

// Enable Swagger middleware (directly)
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DeviceControl API V1");
});

// Configure endpoints
app.MapGet("/devices", (IDeviceRepository repository) =>
{
    var devices = repository.GetAllDevices();
    return Results.Ok(devices);
});

app.MapGet("/devices/{id}", (string id, IDeviceRepository repository) =>
{
    var device = repository.GetDeviceById(id);
    if (device == null)
        return Results.NotFound();
    return Results.Ok(device);
});

app.MapPost("/devices", (Device device, IDeviceRepository repository) =>
{
    repository.AddDevice(device);
    return Results.Created($"/devices/{device.Id}", device);
});

app.MapPut("/devices/{id}", (string id, Device updatedDevice, IDeviceRepository repository) =>
{
    var existingDevice = repository.GetDeviceById(id);
    if (existingDevice == null)
        return Results.NotFound();

    repository.UpdateDevice(id, updatedDevice);
    return Results.Ok(updatedDevice);
});

app.MapDelete("/devices/{id}", (string id, IDeviceRepository repository) =>
{
    var device = repository.GetDeviceById(id);
    if (device == null)
        return Results.NotFound();

    repository.DeleteDevice(id);
    return Results.Ok();
});

app.Run();