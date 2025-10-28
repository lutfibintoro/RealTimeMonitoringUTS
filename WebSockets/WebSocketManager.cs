using System;
using System.Diagnostics.CodeAnalysis;
using System.Net.WebSockets;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RealTimeMonitoringUTS.Data;

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

        public static async Task AddNewClientAsync(HttpContext httpContext, ILogger<Program> logger, RealTimeMonitoringDbContext dbContext)
        {
            using (WebSocket webSocket = await httpContext.WebSockets.AcceptWebSocketAsync(new WebSocketAcceptContext() { DangerousEnableCompression = true }))
            {
                WebSocketClients client = new(webSocket, new byte[1024 * 4]);
                Clients.Add(client);
                await client.EchoAsync(logger, () => Clients.Remove(client), dbContext);
            }
        }

        public static bool TryParseJson<T>(string jsonText, [MaybeNullWhen(false)] out T result)
        {
            try
            {
                result = JsonSerializer.Deserialize<T>(jsonText)!;
                return true;
            }
            catch (JsonException)
            {
                result = default;
                return false;
            }
        }
    }
}
