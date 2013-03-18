using Microsoft.AspNet.SignalR;

namespace Strudle.Conns
{
    public class Chat : Hub
    {
        public void Send(string message)
        {
            // Call the addMessage method on all clients            
            Clients.All.addMessage(string.Format("[{0}] says: {1}", Context.ConnectionId, message));
        }
    }
}