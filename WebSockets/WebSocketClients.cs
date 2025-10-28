using System;
using System.Net.WebSockets;
using System.Text;
using RealTimeMonitoringUTS.Data;
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
        public async Task EchoAsync(ILogger<Program> logger, Action remove, RealTimeMonitoringDbContext dbContext)
        {
            try
            {
                _receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);
                await _webSocket.SendAsync(
                    new ArraySegment<byte>(_buffer, 0, _receiveResult.Count),
                    WebSocketMessageType.Text,
                    _receiveResult.EndOfMessage,
                    CancellationToken.None);

                while (!_receiveResult.CloseStatus.HasValue)
                {
                    _receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);
                    string receivedMessage = Encoding.UTF8.GetString(_buffer, 0, _receiveResult.Count);

                    if (WebSocketManager.TryParseJson<SensorViewModelL>(receivedMessage, out SensorViewModelL? results))
                    {
                        dbContext.Sensors.Add(new()
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
                        });
                        await dbContext.SaveChangesAsync();
                    }
                    await WebSocketManager.BroadCastAsync(receivedMessage);
                }

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
                remove();
                logger.LogInformation("1 client remove");
            }
        }

        public async Task SendMeMessagesAsync(string messages)
        {
            if (_webSocket.State == WebSocketState.Closed)
                return;

            byte[] message = Encoding.UTF8.GetBytes(messages);
            await _webSocket.SendAsync(
                new ArraySegment<byte>(message, 0, message.Length),
                WebSocketMessageType.Text,
                _receiveResult!.EndOfMessage,
                CancellationToken.None);
        }
    }
}
