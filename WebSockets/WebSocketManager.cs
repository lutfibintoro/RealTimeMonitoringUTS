using System;
using System.Net.WebSockets;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace RealTimeMonitoringUTS.WebSockets
{
    public static class WebSocketManager
    {
        public static readonly List<WebSocketClients> Clients = [];

        public static async Task BroadCastAsync(string message)
        {
            foreach (WebSocketClients client in Clients)
            {
                await client.SendMeMessagesAsync(message);
            }
        }

        public static async Task AddNewClientAsync(HttpContext httpContext, ILogger<Program> logger)
        {
            using (WebSocket webSocket = await httpContext.WebSockets.AcceptWebSocketAsync(new WebSocketAcceptContext() { DangerousEnableCompression = true }))
            {
                WebSocketClients client = new(webSocket, new byte[1024 * 4]);
                Clients.Add(client);
                await client.EchoAsync(logger, () => Clients.Remove(client));
            }
        }
    }
}
