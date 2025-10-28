using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RealTimeMonitoringUTS.Data;
using RealTimeMonitoringUTS.Data.Model;
using RealTimeMonitoringUTS.Models;

namespace RealTimeMonitoringUTS.Endpoints
{
    public static class ArduinoDataRequestEndpoints
    {
        public static WebApplication MapArduinoDataRequestEndpoints(this WebApplication app)
        {
            app.MapPost("/arduino", async (
                [FromQuery] double temperatureC,
                [FromQuery] double humidity,
                [FromQuery] double methaneGas,
                [FromQuery] double hydrogenGas,
                [FromQuery] double smoke,
                [FromQuery] double lpgGas,
                [FromQuery] double alcohonGas,
                [FromQuery] int x,
                [FromQuery] int y,
                [FromQuery] int z,
                [FromServices] RealTimeMonitoringDbContext dbContext) =>
            {
                SensorViewModel sensor = new()
                {
                    TemperatureC = temperatureC,
                    Humidity = humidity,
                    MethaneGas = methaneGas,
                    HydrogenGas = hydrogenGas,
                    Smoke = smoke,
                    LpgGas = lpgGas,
                    AlcohonGas = alcohonGas,
                    X = x,
                    Y = y,
                    Z = z,
                    AddAt = DateTime.Now
                };
                await WebSockets.WebSocketManager.BroadCastAsync(JsonSerializer.Serialize(sensor));


                dbContext.Sensors.Add(new()
                {
                    TemperatureC = sensor.TemperatureC,
                    Humidity = sensor.Humidity,
                    MethaneGas = sensor.MethaneGas,
                    HydrogenGas = sensor.HydrogenGas,
                    Smoke = sensor.Smoke,
                    LpgGas = sensor.LpgGas,
                    AlcohonGas = sensor.AlcohonGas,
                    X = sensor.X,
                    Y = sensor.Y,
                    Z = sensor.Z,
                    AddAt = sensor.AddAt
                });
                await dbContext.SaveChangesAsync();

                return TypedResults.Created();
            });

            app.MapDelete("/arduino", async ([FromServices] RealTimeMonitoringDbContext dbContext) =>
            {
                foreach (Sensor sensor in dbContext.Sensors)
                    dbContext.Sensors.Remove(sensor);
                await dbContext.SaveChangesAsync();

                return TypedResults.NoContent();
            });

            app.MapGet("/arduino", async (
                [FromQuery] int limit,
                [FromServices] RealTimeMonitoringDbContext dbContext) =>
            {
                return TypedResults.Ok(
                    await dbContext.Sensors
                    .OrderByDescending(e => e.AddAt)
                    .Take(limit)
                    .AsNoTracking()
                    .ToListAsync());
            });

            return app;
        }
    }
}
