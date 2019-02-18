using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace Mqtt.CommonLib
{
    public class MqttDoubleChannelClient
    {
        private readonly string _ip;
        private readonly int _port;
        private readonly string _topic;
        private readonly IMqttClient _client = new MqttFactory().CreateMqttClient();
        private readonly List<string> _messages = new List<string>();

        public MqttDoubleChannelClient(string ip, int port, string topic)
        {
            _client.ApplicationMessageReceived += (s, e) =>
            {
                _messages.Add(Encoding.UTF8.GetString(e.ApplicationMessage.Payload) + " | " + e.ApplicationMessage.Topic);
            };

            _ip = ip;
            _port = port;
            _topic = topic;

            Connect().Wait();
        }

        private async Task<MqttClientConnectResult> Connect()
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(_ip, _port)
                .Build();

            return await _client.ConnectAsync(options);
        }

        private async void Subscribe()
        {
            await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(_topic).Build());
        }

        private async void Publish(string message)
        {
            var mqttApplicationMessageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(_topic)
                .WithPayload(message)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            await _client.PublishAsync(mqttApplicationMessageBuilder);
        }

        private string GetAllMessages()
        {
            var stringBuilder = new StringBuilder();

            foreach (var m in _messages)
            {
                stringBuilder.AppendLine(m + " | ");
                stringBuilder.AppendLine();
            }

            return stringBuilder.ToString();
        }

        public static void SendMessage(string ip, int port, string topic, string message)
        {
            var client = new MqttDoubleChannelClient(ip, port, topic);

            client.Publish(message);
        }

        public static string GetMessages(string ip, int port, string topic)
        {
            var client = new MqttDoubleChannelClient(ip, port, topic);

            client.Subscribe();

            return client.GetAllMessages();
        }
    }
}
