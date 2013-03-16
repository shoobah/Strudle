using Microsoft.AspNet.SignalR;

namespace Strudle.Conns
{
    public class Connection : PersistentConnection
    {
        protected override System.Threading.Tasks.Task OnReceived(IRequest request, string connectionId, string data)
        {
            var t = new { Id = data.Length, Name = data, ConnId = connectionId };
            return Connection.Broadcast(t);
        }
    }
}