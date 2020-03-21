using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebServerBeetCore.SignalR
{
    public class ChatHub : Hub
    {
        public void SendToAll(string id, string message,string idD,string idReciver)
        {
            Clients.All.SendAsync("sendToAll", id, message,idD, idReciver);
        }
    }
}
