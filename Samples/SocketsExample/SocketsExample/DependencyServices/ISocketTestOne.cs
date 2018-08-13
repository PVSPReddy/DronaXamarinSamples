using System;
using System.Threading.Tasks;

namespace SocketsExample
{
    public interface ISocketTestOne
    {
        Task<bool> InitalizeSocket();
        Task<bool> StopSocket();
        Task<bool> SendMessage(string message);
    }
}
