using System;
using System.Text;
using MQTTnet;
using MQTTnet.Client;
using MQTTnet.Extensions.ManagedClient;

namespace Mqtt.Client
{
    public class MqttDualTopicClient
    {
        private readonly string _topicSender;
        private readonly IMqttClient _client = new MqttFactory().CreateMqttClient();

        public MqttDualTopicClient(string ip, int port, string topicReceiver, string topicSender, Func<string, string> messageReceivedHandler)
        {
            _topicSender = topicSender;
            _client.ApplicationMessageReceived += (s, mqttEventArgs) =>
            {
                var message = Encoding.UTF8.GetString(mqttEventArgs.ApplicationMessage.Payload);

                messageReceivedHandler(message);
            };

            Start(ip, port, topicReceiver);
        }

        private async void Start(string ip, int port, string topicReceiver)
        {
            var options = new MqttClientOptionsBuilder()
                .WithTcpServer(ip, port)
                .Build();

            await _client.ConnectAsync(options);
            await _client.SubscribeAsync(new TopicFilterBuilder().WithTopic(topicReceiver).Build());
        }

        public async void Send(string message)
        {
            var mqttApplicationMessageBuilder = new MqttApplicationMessageBuilder()
                .WithTopic(_topicSender)
                .WithPayload(message)
                .WithExactlyOnceQoS()
                .WithRetainFlag()
                .Build();

            await _client.PublishAsync(mqttApplicationMessageBuilder);
        }
    }
}
