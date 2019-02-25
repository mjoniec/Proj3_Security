using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace Mqtt.CommonLib
{
    public class MqttDoubleChannelClientAsync
    {
        private readonly IMqttClient _client = new MqttFactory().CreateMqttClient();
        private readonly List<string> _messages = new List<string>();

        public MqttDoubleChannelClientAsync(Func<string, int> messageReceivedHandler)
        {
            _client.ApplicationMessageReceived += (s, mqttEventArgs) =>
            {
                var message = Encoding.UTF8.GetString(mqttEventArgs.ApplicationMessage.Payload) + " | " + mqttEventArgs.ApplicationMessage.Topic;

                _messages.Add(message);
                messageReceivedHandler(message);
            };
        }

        public async void Start(string ip, int port, string topicReceiver, string topicSender, string topicSenderOpeningMessage)
        {
            await Connect(ip, port);
            await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topicReceiver).Build());
            Publish(topicSender, topicSenderOpeningMessage);
        }

        private async Task<MqttClientConnectResult> Connect(string ip, int port)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(ip, port)
                .Build();

            return await _client.ConnectAsync(options);
        }

        private async void Publish(string topic, string message)
        {
            var mqttApplicationMessageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(topic)
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

        //public static async void Start(MqttDoubleChannelClientAsync client, string ip, int port, string topic, string message)
        //{
        //    await client.Connect(ip, port);
        //    client.Publish(topic, message);
        //}

        public static async void SendMessage(string ip, int port, string topic, string message, Func<string, int> messageReceivedHandler)
        {
            var client = new MqttDoubleChannelClientAsync(messageReceivedHandler);

            await client.Connect(ip, port);
            client.Publish(topic, message);
        }

        public static async Task<string> GetMessages(string ip, int port, string topic, Func<string, int> messageReceivedHandler)
        {
            var client = new MqttDoubleChannelClientAsync(messageReceivedHandler);

            await client.Connect(ip, port);
            client.Subscribe(topic);

            return client.GetAllMessages();
        }
    }
}
