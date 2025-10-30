using System;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Logging;
using RealTimeMonitoringUTS.Data;
using RealTimeMonitoringUTS.Data.Model;
using RealTimeMonitoringUTS.Models;

namespace RealTimeMonitoringUTS.WebSockets
{
    public class WebSocketClients
    {
        private WebSocket _webSocket;
        private WebSocketReceiveResult? _receiveResult;
        private byte[] _buffer;
        private readonly object _lock = new();



        public WebSocket WebSocket { get => _webSocket; }
        public WebSocketReceiveResult? ReceiveResult { get => _receiveResult; set { _receiveResult = value; } }
        public byte[] Buffer { get => _buffer; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="webSocket"></param>
        /// <param name="buffer"></param>
        public WebSocketClients(WebSocket webSocket, byte[] buffer)
        {
            _webSocket = webSocket;
            _buffer = buffer;
        }



        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task EchoAsync(ILogger<Program> logger, RealTimeMonitoringDbContext dbContext)
        {
            try
            {
                _receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);
                await _webSocket.SendAsync(
                    new ArraySegment<byte>(_buffer, 0, _receiveResult.Count),
                    WebSocketMessageType.Text,
                    _receiveResult.EndOfMessage,
                    CancellationToken.None);
                WebSocketManager.Clients.Add(this);

                while (!_receiveResult.CloseStatus.HasValue)
                {
                    string receivedMessage;
                    using (MemoryStream memoryStream = new())
                    {
                        do
                        {
                            _receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);
                            memoryStream.Write(_buffer, 0, _receiveResult.Count);
                        }
                        while (!_receiveResult.EndOfMessage);

                        memoryStream.Seek(0, SeekOrigin.Begin);
                        using (StreamReader reader = new(memoryStream, Encoding.UTF8))
                            receivedMessage = await reader.ReadToEndAsync();
                    }

                    if (WebSocketManager.TryParseJson<SensorViewModelL>(receivedMessage, out SensorViewModelL? results))
                    {
                        Sensor sensor = new()
                        {
                            TemperatureC = results.TemperatureC,
                            Humidity = results.Humidity,
                            MethaneGas = results.MethaneGas,
                            HydrogenGas = results.HydrogenGas,
                            Smoke = results.Smoke,
                            LpgGas = results.LpgGas,
                            AlcohonGas = results.AlcohonGas,
                            X = results.X,
                            Y = results.Y,
                            Z = results.Z,
                            AddAt = new(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second)
                        };

                        dbContext.Sensors.Add(sensor);
                        _ = Task.Run(async () =>
                        {
                            await dbContext.SaveChangesAsync();
                        });

                        await WebSocketManager.BroadCastAsync(JsonSerializer.Serialize(sensor));
                    }
                    else
                    {
                        await WebSocketManager.BroadCastAsync(receivedMessage);
                    }
                }

                WebSocketManager.Clients.Remove(this);
                await Task.Delay(3000);
                await _webSocket.CloseAsync(
                    _receiveResult.CloseStatus.Value,
                    _receiveResult.CloseStatusDescription,
                    CancellationToken.None);
            }
            catch (WebSocketException ex)
            {
                logger.LogWarning("WebSocketException: " + ex.Message);
                logger.LogWarning(ex.StackTrace);
            }
            catch (Exception ex)
            {
                logger.LogError("another ex: " + ex.Message);
            }
            finally
            {
                WebSocketManager.Clients.Remove(this);
                logger.LogInformation("1 client remove");
            }
        }

        public async Task SendMeMessagesAsync(string messages)
        {
            try
            {
                if (_webSocket.State == WebSocketState.Closed || !WebSocketManager.Clients.Contains(this))
                    return;

                byte[] message = Encoding.UTF8.GetBytes(messages);
                await _webSocket.SendAsync(
                    new ArraySegment<byte>(message, 0, message.Length),
                    WebSocketMessageType.Text,
                    true,
                    CancellationToken.None);
            }
            catch (Exception)
            {
            }
        }
    }
}
