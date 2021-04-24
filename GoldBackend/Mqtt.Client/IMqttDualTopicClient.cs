using System;
using System.Threading.Tasks;

namespace Mqtt.Client
{
    public interface IMqttDualTopicClient
    {
        event EventHandler<MessageEventArgs> RaiseMessageReceivedEvent;
        Task<bool> Start();
        void Send(string message);
    }
}
