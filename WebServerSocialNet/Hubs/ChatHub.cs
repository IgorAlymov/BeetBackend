using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using WebServerSocialNet.DataAccessLayer;

namespace AspWebChatSignalR.Hubs
{
    public class ChatHub : Hub
    {
        private static List<SimpleUser> _connectedUsers 
            = new List<SimpleUser>();

        public ChatHub()
        {
            
        }

        public void SendPrivateMessage(string author,
                                       string receiver,
                                       string message)
        {
            var client = _connectedUsers
                .FirstOrDefault(x => x.FirstName == receiver);

            var senderId = Context.ConnectionId;

            if (client != null)
            {
                Clients
                    .Client(client.ConnectId)
                    .addMessage(author, message);

                Clients
                    .Client(senderId)
                    .addMessage("server", "доставлено");
            } else
            {
                Clients
                    .Client(senderId)
                    .addMessage("server", "не доставлено");
            }
        }

        public void SendPublicMessage(string author,
                                      string message)
        {
            var user = _connectedUsers
                .FirstOrDefault(x => x.FirstName == author);
            if (user == null)
                return;
            if (user.ConnectId != Context.ConnectionId)
                return;

            Clients.All.addMessage(author, message);
        }

        public void ConnectUser(string userName)
        {
            // идентификатор соединения (АйПи адрес)
            var id = Context.ConnectionId;

            var user = _connectedUsers
                .FirstOrDefault(x => x.FirstName == userName);
            if (user != null)
            {
                // регистрировать не будем!
                Clients.Caller.addMessage("server",
                    "имя уже занято!");
                return;
            }
            
            // отправляем юзеру список всех клиентов чата
            Clients.Caller.onConnected(id, userName, _connectedUsers);
            // отправляем всем кто в чате нового
            Clients.AllExcept(id).onNewUserConnected(id, userName);

            _connectedUsers.Add(new SimpleUser()
            {
                FirstName = userName,
                ConnectId = id
            });
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            var user = _connectedUsers
                .FirstOrDefault(
                x => x.ConnectId == Context.ConnectionId);

            if (user != null)
            {
                _connectedUsers.Remove(user);
                var id = user.ConnectId;
                Clients.All.onUserDisconnected(id, user.FirstName);
            }
            return base.OnDisconnected(stopCalled);
        }
    }
}