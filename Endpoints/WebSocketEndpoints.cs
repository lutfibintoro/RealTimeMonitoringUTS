using System.Net.WebSockets;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using RealTimeMonitoringUTS.Data;
using RealTimeMonitoringUTS.Data.Model;

namespace RealTimeMonitoringUTS.Endpoints
{
    public static class WebSocketEndpoints
    {
        public static WebApplication MapWebSocketEndpoints(this WebApplication app)
        {
            app.MapGet("/ws/monitor", async (HttpContext httpContext, ILogger<Program> logger, RealTimeMonitoringDbContext dbContext) =>
            {
                if (!httpContext.WebSockets.IsWebSocketRequest)
                {
                    httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return;
                }

                await WebSockets.WebSocketManager.AddNewClientAsync(httpContext, logger, dbContext);
            });


            return app;
        }
    }
}
