using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;
using System.Net.WebSockets;
using System.Threading;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Http;
using System.Text;

namespace CA.Services.AuthoringService.API.Controllers
{
    public class AuthoringWebsocketController
    {
        private ConcurrentDictionary<string, WebSocket> _sockets = new ConcurrentDictionary<string, WebSocket>();

        public ConcurrentDictionary<string, WebSocket> GetAllSockets()
        {
            return _sockets;
        }

        public string AddSocket(WebSocket socket)
        {
            string connectionId = Guid.NewGuid().ToString();

            _sockets.TryAdd(connectionId, socket);
            Console.WriteLine("Connection Added: " + connectionId);

            return connectionId;
        }
    }
}
