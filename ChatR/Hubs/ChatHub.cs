using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;
using System.Threading.Tasks;
using ChatR.Models;

namespace ChatR.Hubs
{
    public class ChatHub : Hub
    {
        public void Send(Message message)
        {
            // Call the addNewMessageToPage method to update clients.
            Clients.All.addNewMessageToPage(message);
        }

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected()
        {
            return base.OnDisconnected();
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

    }
}