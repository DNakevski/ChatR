using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using ChatR.Models;
using ChatR.Entities;
using ChatR.Persistence;
using ChatR.WorkerService;
using ChatR.Infrastructure;

namespace ChatR.Hubs
{
    public class ChatHub : Hub
    {
        private readonly static UserConnectionMapping<int> _connections = UserConnectionMapping<int>.GetInstance();
        private readonly ClientNotifier _clientNotifier;

        public ChatHub()
        {
            _clientNotifier = new ClientNotifier();
        }

        public void Send(ChatMessageModel message)
        {
            Clients.All.addNewMessageToPage(message);
        }

        public List<ConnectedUserModel> GetAllConectedUsers()
        {
            List<int> keys = _connections.GetKeys().ToList();

            using (var userService = new UserWorkerService())
            {
                var users = userService.GetConnectedUsersByIds(keys);
                return users;
            }
        }

        #region Connection Events
        public override Task OnConnected()
        {
            string connectionId = Context.ConnectionId;
            int userId = Context.User.Identity.GetUserId<int>();
            _connections.Add(userId, connectionId);
            _clientNotifier.NewUserConnected(userId);
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string connectionId = Context.ConnectionId;
            int userId = Context.User.Identity.GetUserId<int>();
            _connections.Remove(userId, connectionId);
            _clientNotifier.UserDisconnected(userId);

            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            string connectionId = Context.ConnectionId;
            int userId = Context.User.Identity.GetUserId<int>();

            if (!_connections.GetConnections(userId).Contains(connectionId))
            {
                _connections.Add(userId, connectionId);
            }

            return base.OnReconnected();
        }
        #endregion

    }
}