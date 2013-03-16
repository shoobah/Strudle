using Microsoft.AspNet.SignalR;

namespace Strudle.Conns
{
    public class TheHub : Hub
    {
        public string Send(string data)
        {
            return data;
        }
    }
}