using System;
using System.Net.WebSockets;
using System.Text;

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
        public async Task EchoAsync(ILogger<Program> logger, Action remove)
        {
            try
            {
                _receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);

                while (!_receiveResult.CloseStatus.HasValue)
                {
                    await _webSocket.SendAsync(
                        new ArraySegment<byte>(_buffer, 0, _receiveResult.Count),
                        WebSocketMessageType.Text,
                        _receiveResult.EndOfMessage,
                        CancellationToken.None);

                    _receiveResult = await _webSocket.ReceiveAsync(new ArraySegment<byte>(_buffer), CancellationToken.None);
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
