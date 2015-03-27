using ChatR.Hubs;
using ChatR.Persistence;
using ChatR.WorkerService;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ChatR.Infrastructure
{
    public class ClientNotifier
    {
        private readonly static UserConnectionMapping<int> _connections = UserConnectionMapping<int>.GetInstance();
        private IHubContext _chatContext;

        public ClientNotifier()
        {
            _chatContext = GlobalHost.ConnectionManager.GetHubContext<ChatHub>();
        }

        public void NewUserConnected(int userId)
        {
            //get the user from the database
            using(var service = new UserWorkerService())
            {
                try
                {
                    var connectedUser = service.GetConnectedUserById(userId);
                    var currentConnection = _connections.GetConnections(userId);
                    _chatContext.Clients.AllExcept(currentConnection.ToArray()).newUserConnected(connectedUser);
                }
                catch (Exception ex) { }
            }
            
        }

        public void UserDisconnected(int userId)
        {
            var currentConnection = _connections.GetConnections(userId);
            _chatContext.Clients.AllExcept(currentConnection.ToArray()).userDisconnected(userId);
        }
    }
}