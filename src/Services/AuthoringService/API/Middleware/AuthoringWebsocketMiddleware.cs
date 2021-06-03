using CA.Services.AuthoringService.API.Controllers;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CA.Services.AuthoringService.API.Middleware
{
    public class AuthoringWebsocketMiddleware
    {
        private readonly RequestDelegate _next;

        private readonly AuthoringWebsocketController _controller;

        public AuthoringWebsocketMiddleware(RequestDelegate next, AuthoringWebsocketController controller)
        {
            _next = next;
            _controller = controller;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            //WriteRequestParam(context);

            if (context.WebSockets.IsWebSocketRequest)
            {
                WebSocket webSocket = await context.WebSockets.AcceptWebSocketAsync();

                Console.WriteLine("Websocket Connected");

                string connectionId = _controller.AddSocket(webSocket);
                
                await SendConnectionIdAsync(webSocket, connectionId);

                await ReceiveMessage(webSocket, async (result, buffer) =>
                {
                    if (result.MessageType == WebSocketMessageType.Text)
                    {
                        Console.WriteLine($"Message Received : {Encoding.UTF8.GetString(buffer, 0, result.Count)}");
                        return;
                    }
                    else if (result.MessageType == WebSocketMessageType.Close)
                    {
                        Console.WriteLine("Received Close Message");
                        return;
                    }
                });
            }
            else
            {
                Console.WriteLine("Websocktet not a Websocket Request delegate");
                await _next(context);
            }
        }

        private async Task SendConnectionIdAsync(WebSocket socket, string id)
        {
            var buffer = Encoding.UTF8.GetBytes("id: "+ id);
            await socket.SendAsync(buffer, WebSocketMessageType.Text, true, cancellationToken: CancellationToken.None);
        }

        private async Task ReceiveMessage(WebSocket socket, Action<WebSocketReceiveResult, byte[]> handleMessage)
        {
            byte[] buffer = new byte[1024 * 4];

            while (socket.State == WebSocketState.Open)
            {
                var result = await socket.ReceiveAsync(buffer: new ArraySegment<byte>(buffer), cancellationToken: CancellationToken.None);

                handleMessage(result, buffer);
            }
        }
    }
}
